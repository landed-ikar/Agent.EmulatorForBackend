using Agent.EmulatorForBackend.ViewModels;
using Monq.Core.Models.Agents.Scheduler;
using Monq.Core.Models.Agents.Types;

namespace Agent.EmulatorForBackend.DesignTime;
public class DesignAgentTaskViewModel : IAgentTaskViewModel
{
    const string DefaultScript =
        """
        name: Zabbix - Version Check
        jobs:
          - name: Check version
            steps:
              - plugin: zabbixCheckVersion
                with:
                  streamId: $.vars.stream.id
                  apiUri: $.vars.stream.params.apiUri
                  login: $.vars.stream.params.login
                  insecureMode: $.vars.stream.params.insecureMode
                  timeout: $.storage.timeout
                with-secured:
                  password: $.vars.stream.params.password
                  auth: $.storage.auth
                outputs:
                  version: '{ "Version": "{{ _outputs.version }}" }'
                store:
                  auth: $._outputs.auth
            artifacts:
              - name: streamLabels
                send-to:
                  system:
                    keys:
                      - cl.stream.labels-update
                data: '{ "labels": {{ outputs.version }}, "streamId": {{ vars.stream.id }} }'
        """;
    public Guid TaskEventId { get; set; } = Guid.Empty;

    public AgentTaskInfo TaskInfo { get; set; } = new AgentTaskInfo()
    {
        Id = 123456,
        Name = "Name",
        Script = DefaultScript,
        HolderInfo = new AgentTaskHolderInfo()
        {
            Id = 654321,
            Type = AgentTaskHolderTypes.Stream
        },
        Labels = new string[] {"Label_1", "Label_2", "Label_3" }
    };

    public RelayCommand CompleteOkCommand => throw new NotImplementedException();

    public RelayCommand CompleteErrorCommand => throw new NotImplementedException();

    public event EventHandler<EventArgs> TaskCompleted;
}
