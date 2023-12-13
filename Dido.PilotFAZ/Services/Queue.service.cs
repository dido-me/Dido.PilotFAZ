using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace Dido.PilotFAZ.Services;

public static class QueueService
{
    private static CloudQueue _queue;

    public static async Task<CloudQueue> PrepareQueue(string queueName = null)
    {
        var connection = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
        var storageAccount = CloudStorageAccount.Parse(connection);
        var queueClient = storageAccount.CreateCloudQueueClient();
        _queue = queueClient.GetQueueReference(queueName ?? throw new ArgumentNullException(nameof(queueName)));
        await _queue.CreateIfNotExistsAsync();
        
        return _queue;
    }
}