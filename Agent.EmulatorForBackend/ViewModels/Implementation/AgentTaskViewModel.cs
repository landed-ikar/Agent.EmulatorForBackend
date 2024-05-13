using Microsoft.AspNetCore.SignalR.Client;
using Monq.Core.Models.Agents.Scheduler;
using Monq.Core.Models.Agents.Types;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static Agent.EmulatorForBackend.Configuration.AppConstants;

namespace Agent.EmulatorForBackend.ViewModels.Implementation;
public class AgentTaskViewModel : IAgentTaskViewModel, INotifyPropertyChanged
{
    readonly HubConnection _connection;

    private Guid taskEventId;
    private RelayCommand? completeOkCommand;
    private RelayCommand? completeErrorCommand;
    public event PropertyChangedEventHandler? PropertyChanged;
    public event EventHandler<EventArgs> TaskCompleted;
    
    public Guid TaskEventId
    {
        get => taskEventId;
        set
        {
            taskEventId = value;
            OnPropertyChanged();
        }
    }
    public AgentTaskInfo TaskInfo { get; set; } = new();
    public RelayCommand CompleteOkCommand
    {
        get
        {
            return completeOkCommand ??
              (completeOkCommand = new RelayCommand(async obj =>
              {
                  var result = GetOkResult();
                  _ = _connection.SendAsync(Scheduler.HandleTaskResult, result).ContinueWith(OnTaskCompleted);
              }));
        }
    }
    public RelayCommand CompleteErrorCommand
    {
        get
        {
            return completeErrorCommand ??
              (completeErrorCommand = new RelayCommand(obj =>
              {
                  var result = GetFailResult();
                  _ = _connection.SendAsync(Scheduler.HandleTaskResult, result).ContinueWith(OnTaskCompleted);
              }));
        }
    }

    public AgentTaskViewModel(HubConnection connection)
    {
        _connection = connection;
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    public void OnTaskCompleted(Task _)
    {
        if (TaskInfo.DurationMode == AgentTaskDurationModes.Continuous)
            return;
        if (TaskCompleted != null)
            TaskCompleted(this, EventArgs.Empty);
    }

    private AgentTaskResultEvent GetOkResult() =>
        new AgentTaskResultEvent()
        {
            Id = taskEventId,
            Message = "Ok",
            Status = AgentTaskExecutingStatus.Ok,
            Timestamp = DateTimeOffset.Now,
            TaskId = TaskInfo.Id,
            UserspaceId = TaskInfo.UserspaceId,
        };

    private AgentTaskResultEvent GetFailResult() =>
        new AgentTaskResultEvent()
        {
            Id = taskEventId,
            Message = "Error",
            Status = AgentTaskExecutingStatus.Error,
            Timestamp = DateTimeOffset.Now,
            TaskId = TaskInfo.Id,
            UserspaceId = TaskInfo.UserspaceId,
        };
}
