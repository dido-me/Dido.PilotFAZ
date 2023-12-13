using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dido.PilotFAZ.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Dido.PilotFAZ;

public static class TimerF1
{
    [FunctionName("TimerF1")]
    public static async Task RunAsync([TimerTrigger("*/10 * * * * *")] TimerInfo myTimer, ILogger log)
    {
        try
        {
            var queueName = Environment.GetEnvironmentVariable("QUEUE_NAME");

            var users = await ExpressnetService.FetchUserData();
            
            var queue = await QueueService.PrepareQueue(queueName);

            var message = new CloudQueueMessage(users);
            await queue.AddMessageAsync(message);
        }
        catch (Exception e)
        {
            log.LogError($"Error {e}");
        }
    }
}