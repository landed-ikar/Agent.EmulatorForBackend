using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Monq.Core.Models.Agents.Scheduler;
using System.Net.Http;
using Agent.EmulatorForBackend.Configuration;
using Agent.EmulatorForBackend.Exceptions;
using static Agent.EmulatorForBackend.Configuration.AppConstants;

namespace Agent.EmulatorForBackend.Services.Implementation;
public class ConnectionManager : IConnectionManager
{
    static readonly TimeSpan _hubReconnectionTimeout = TimeSpan.FromSeconds(5);

    private readonly ILogger<ConnectionManager> _logger;

    public ConnectionManager(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ConnectionManager>();
    }
    public HubConnection CreateHubConnection(string baseUri)
    {
        _logger.LogInformation($"Hub connection {baseUri}");
        var url = new Uri(new Uri(baseUri), Scheduler.HubRoute).ToString();
        var connection = new HubConnectionBuilder()
            .AddNewtonsoftJsonProtocol()
            .WithUrl(url, (opts) =>
            {
                opts.HttpMessageHandlerFactory = (message) =>
                {
                    if (message is HttpClientHandler clientHandler)
                        clientHandler.ServerCertificateCustomValidationCallback +=
                            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                    return message;
                };
            })
            .Build();
        connection.Closed += (error) =>
        {
            if (error is null)
                _logger.LogError(new EventId(), "Connection terminated.");
            else
                _logger.LogError(new EventId(), error, "Connection terminated. Reason: {0}", error.Message);
            return Task.CompletedTask;
        };

        //connection.On<AgentTaskEvent>(Commands.DoTask, taskEvent => DoTask(taskEvent));
        //connection.On<CancelTaskEvent>(Commands.CancelTask, cancelEvent => CancelTask(cancelEvent));
        return connection;
    }

    public async Task<AgentRegistrationResultType> EstablishConnection(
        HubConnection hubConnection, AgentInfo agentInfo, CancellationToken cancellationToken)
    {
        var reconnectionAttempts = 0;

        while (hubConnection.State == HubConnectionState.Disconnected)
        {
            _logger.LogInformation("Establishing connection.");

            try
            {
                var connectionTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(AppConstants.Connection.Timeout));
                var connectionToken = connectionTokenSource.Token;
                await hubConnection.StartAsync(connectionToken);

                var result = await hubConnection.InvokeAsync<AgentRegistrationResult>(AppConstants.Scheduler.Register, agentInfo, connectionToken);
                if (result.Result == AgentRegistrationResultType.Failure)
                {
                    await hubConnection.StopAsync(connectionToken);
                    throw new RegistrationFailedException(result.Message);
                }

                _logger.LogInformation("Connection established.");
                return result.Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(), ex, ex.Message, ex);

                reconnectionAttempts++;
                if (reconnectionAttempts > AppConstants.Connection.RetryCount)
                {
                    _logger.LogError("Connection failed. Number of connection attempts exceeded.");
                    break;
                }

                await Task.Delay(_hubReconnectionTimeout, cancellationToken);
            }
        }
        return AgentRegistrationResultType.Failure;
    }
}
