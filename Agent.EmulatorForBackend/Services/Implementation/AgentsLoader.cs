using Agent.EmulatorForBackend.Configuration;
using Agent.EmulatorForBackend.Models;
using Agent.EmulatorForBackend.ViewModels;
using Agent.EmulatorForBackend.ViewModels.Implementation;
using Mapster;
using System.IO;
using System.Text.Json;

namespace Agent.EmulatorForBackend.Services.Implementation;
public class AgentsLoader : IAgentsLoader
{
    readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
    {
        WriteIndented = true,
    };
    readonly IAgentViewModelBuilder _builder;
    public AgentsLoader(IAgentViewModelBuilder builder)
    {
        _builder = builder;
    }
    public async Task<IEnumerable<IAgentViewModel>> Load()
    {
        if (!File.Exists(AppConstants.Storage.AgentsFilePath))
            return Enumerable.Empty<AgentViewModel>();

        using var fs = File.OpenRead(AppConstants.Storage.AgentsFilePath);
        var agentModels = await JsonSerializer.DeserializeAsync<AgentStoredModel[]>(fs)
            ?? Enumerable.Empty<AgentStoredModel>();
        var agentViewModels = agentModels.Select(x =>
        {
            var agentViewModel = _builder.Build();
            x.Adapt(agentViewModel);
            return agentViewModel;
        }).ToList();

        return agentViewModels;
    }

    public async Task Save(IEnumerable<IAgentViewModel> agents)
    {
        if(!Directory.Exists(AppConstants.Storage.StoragePath))
            Directory.CreateDirectory(AppConstants.Storage.StoragePath);

        using var fs = File.Open(AppConstants.Storage.AgentsFilePath, FileMode.Create);
        var agentStoredModels = agents.Adapt<AgentStoredModel[]>();
        await JsonSerializer.SerializeAsync(fs, agentStoredModels, _jsonOptions);
        await fs.FlushAsync();
    }
}
