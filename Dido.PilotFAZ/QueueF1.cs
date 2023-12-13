using System.Threading.Tasks;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Dido.PilotFAZ.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Dido.PilotFAZ.Services;

namespace Dido.PilotFAZ;

public static class QueueF1
{
    private static readonly HttpClient Client = new HttpClient();

    [FunctionName("QueueF1")]
    public static async Task RunAsync([QueueTrigger("%QUEUE_NAME%")] string myQueueItem, ILogger log)
    {
        var users = JsonSerializer.Deserialize<User[]>(myQueueItem);
        var emails = users.Select(user => user.Email).ToList();

        try
        {
            await Smtp2GoService.SendMails(emails);
            log.LogInformation($"Se enviaron {emails.Count} correos");
        }
        catch (Exception e)
        {
            log.LogError($"Error al deserializar el JSON: {e.Message}");
        }
    }
}