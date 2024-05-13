using Agent.EmulatorForBackend;
using Agent.EmulatorForBackend.Builders.Implementation;
using Agent.EmulatorForBackend.Configuration;
using Agent.EmulatorForBackend.Configuration.MapsterProfiles;
using Agent.EmulatorForBackend.Services;
using Agent.EmulatorForBackend.Services.Implementation;
using Agent.EmulatorForBackend.ViewModels;
using Agent.EmulatorForBackend.ViewModels.Implementation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    [STAThread]
    public static void Main()
    {
        TypeAdapterConfig.GlobalSettings.Apply(new AgentModelProfile());
        TypeAdapterConfig.GlobalSettings.Apply(new AgentTaskModelProfile());


        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IAgentsLoader, AgentsLoader>();
                services.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
                services.AddSingleton<IAgentViewModelBuilder,AgentViewModelBuilder>();
                services.AddSingleton<IAgentTaskViewModelBuilder, AgentTaskViewModelBuilder>();
                services.AddSingleton<IConnectionManager, ConnectionManager>();
                services.AddLogging(configure => configure.AddConsole());
            })
            .Build();

        var app = host.Services.GetService<App>();
        app?.Run();
    }
}