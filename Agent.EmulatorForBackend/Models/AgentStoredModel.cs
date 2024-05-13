namespace Agent.EmulatorForBackend.Models;
public class AgentStoredModel
{
    public string Name { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUri { get; set; } = string.Empty;
    public IEnumerable<string> Labels { get; set; } = Array.Empty<string>();
    public string Description { get; set; } = string.Empty;
    public int SlotsCount { get; set; } = 0;

}
