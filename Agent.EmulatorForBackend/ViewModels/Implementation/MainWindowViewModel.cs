using Agent.EmulatorForBackend.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Agent.EmulatorForBackend.ViewModels.Implementation;
public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
{
    ObservableCollection<IAgentViewModel> agents = new();
    readonly IAgentsLoader _agentsLoader;
    readonly IAgentViewModelBuilder _agentViewModelBuilder;

    public ObservableCollection<IAgentViewModel> Agents
    {
        get => agents;
        set
        {
            agents = value;
            OnPropertyChanged();
        }
    }

    RelayCommand? addAgentCommand;

    RelayCommand? deleteAgentCommand;

    RelayCommand? saveAgentsCommand;

    public RelayCommand AddAgentCommand
    {
        get
        {
            return addAgentCommand ??
              (addAgentCommand = new RelayCommand(obj =>
              {
                  Agents.Add(_agentViewModelBuilder.Build());
              }));
        }
    }

    public RelayCommand DeleteAgentCommand
    {
        get
        {
            return deleteAgentCommand ??
              (deleteAgentCommand = new RelayCommand(obj =>
              {
                  Agents.Remove(obj as IAgentViewModel);
              }));
        }
    }

    public RelayCommand SaveAgentsCommand
    {
        get
        {
            return saveAgentsCommand ??
              (saveAgentsCommand = new RelayCommand(obj =>
              {
                  _agentsLoader.Save(Agents);
              }));
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

    public MainWindowViewModel(IAgentsLoader loader, IAgentViewModelBuilder agentViewModelBuilder)
    {
        _agentViewModelBuilder = agentViewModelBuilder;
        _agentsLoader = loader;
        loader.Load().ContinueWith(HandleLoadAsync);
    }

    void HandleLoadAsync(Task<IEnumerable<IAgentViewModel>> loadTask)
    {
        if (loadTask.IsFaulted)
        {
            return;
        }
        Agents = new ObservableCollection<IAgentViewModel>(loadTask.Result);
    }
}
