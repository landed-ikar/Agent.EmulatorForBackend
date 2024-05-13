using Agent.EmulatorForBackend.Services;
using Mapster;
using Microsoft.AspNetCore.SignalR.Client;
using Monq.Core.Models.Agents.Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static Agent.EmulatorForBackend.Configuration.AppConstants;

namespace Agent.EmulatorForBackend.ViewModels.Implementation;
public class AgentViewModel : IAgentViewModel, INotifyPropertyChanged
{
    private CancellationTokenSource? connectionCancellationToken;
    private string name = string.Empty;
    private string description = string.Empty;
    private string apiKey = string.Empty;
    private string baseUri = string.Empty;
    private int slotsCount = 1;
    private AgentConnectionState connectionState = AgentConnectionState.Disconnected;
    private RelayCommand? connectCommand;
    private RelayCommand? disconnectCommand;
    private HubConnection? _connection;
    private readonly IConnectionManager _connectionManager;
    private readonly IAgentTaskViewModelBuilder _agentTaskBuilder;

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }
    public string Description
    {
        get => description;
        set
        {
            description = value;
            OnPropertyChanged();
        }
    }
    public string ApiKey
    {
        get => apiKey;
        set
        {
            apiKey = value;
            OnPropertyChanged();
        }
    }
    public string BaseUri
    {
        get => baseUri;
        set
        {
            baseUri = value;
            OnPropertyChanged();
        }
    }
    public int SlotsCount
    {
        get => slotsCount;
        set
        {
            slotsCount = value;
            OnPropertyChanged();
        }
    }
    public AgentConnectionState ConnectionState
    {
        get => connectionState;
        set
        {
            connectionState = value;
            OnPropertyChanged();
        }
    }
    public ObservableCollection<string> Labels { get; set; } = new();
    public ObservableCollection<IAgentTaskViewModel> AgentTasks { get; set; } = new();
    public RelayCommand ConnectCommand
    {
        get
        {
            return connectCommand ??
              (connectCommand = new RelayCommand(obj => 
              {
                  connectionCancellationToken = new CancellationTokenSource();
                  _ = Connect(connectionCancellationToken.Token).ConfigureAwait(false); }));
        }
    }
    public RelayCommand DisconnectCommand
    {
        get
        {
            return disconnectCommand ??
              (disconnectCommand = new RelayCommand(obj =>
              {
                  connectionCancellationToken?.Cancel();
                  _connection?.StopAsync();
              }));
        }
    }

    public AgentViewModel(
        IConnectionManager connectionManager,
        IAgentTaskViewModelBuilder agentTaskBuilder)
    {
        _connectionManager = connectionManager;
        _agentTaskBuilder = agentTaskBuilder;
    }

    async Task Connect(CancellationToken cancellationToken)
    {
        ConnectionState =  AgentConnectionState.Connecting;
        try
        {
            _connection = _connectionManager.CreateHubConnection(BaseUri);
            _connection.Closed += (error) =>
            {
                if (ConnectionState != AgentConnectionState.Connected)
                    return Task.CompletedTask;

                System.Windows.Application.Current.Dispatcher.Invoke(delegate
                {
                    ConnectionState = AgentConnectionState.Disconnected;
                    AgentTasks.Clear();
                });
                return Task.CompletedTask;
            };

            _connection.On<AgentTaskEvent>(Scheduler.DoTask, taskEvent => DoTask(taskEvent));
            _connection.On<CancelTaskEvent>(Scheduler.CancelTask, cancelEvent => CancelTask(cancelEvent));

            var agentInfo = this.Adapt<AgentInfo>();
            var connectionResult = await _connectionManager.EstablishConnection(_connection, agentInfo, cancellationToken);
            ConnectionState = connectionResult switch
            {
                AgentRegistrationResultType.Success => AgentConnectionState.Connected,
                AgentRegistrationResultType.Failure => AgentConnectionState.Disconnected,
                _ => throw new NotImplementedException()
            };
        }
        catch
        { 
            ConnectionState = AgentConnectionState.Disconnected;
        }
        finally
        {
            connectionCancellationToken?.Dispose();
            connectionCancellationToken = null;
        }
    }

    void DoTask(AgentTaskEvent taskEvent)
    {
        var agentTaskViewModel = _agentTaskBuilder.Build(_connection);
        agentTaskViewModel.TaskCompleted += (s, e) =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(delegate
                {
                    AgentTasks.Remove(s as AgentTaskViewModel);
                });
            };
        agentTaskViewModel.TaskInfo = taskEvent.TaskInfo;
        agentTaskViewModel.TaskEventId = taskEvent.Id;
        System.Windows.Application.Current.Dispatcher.Invoke(delegate
        {
            AgentTasks.Add(agentTaskViewModel);
        });
    }

    void CancelTask(CancelTaskEvent taskEvent)
    {
        var task = AgentTasks.FirstOrDefault(x => x.TaskEventId == taskEvent.TaskEventId);
        if (task != null)
            System.Windows.Application.Current.Dispatcher.Invoke(delegate
            {
                AgentTasks.Remove(task);
            });
    }
}
