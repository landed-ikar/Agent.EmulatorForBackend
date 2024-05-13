namespace Agent.EmulatorForBackend.Configuration;
public class AppConstants
{
    public static class Storage
    {
        public const string StoragePath = "Data";
        public const string AgentsFilePath = "Data/Agents.json";
    }

    public static class Scheduler
    {
        public const string HubRoute = "api/pl/hubs/scheduler";

        public const string DoTask = "DoTask";
        public const string CancelTask = "CancelTask";

        public const string HandleTaskResult = "HandleTaskResult";
        public const string Register = "Register";
        public const string ReceiveLogs = "ReceiveLogs";
    }


    public static class Connection
    {
        public const int Timeout = 100;
        public const int RetryCount = 10;
    }
}
