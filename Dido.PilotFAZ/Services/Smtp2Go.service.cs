using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Dido.PilotFAZ.Services;

public static class Smtp2GoService
{
    private static readonly HttpClient Client = new HttpClient();

    public static async Task SendMails(List<string> emails)
    {
        var urlApi = $"{Environment.GetEnvironmentVariable("SMTP2GO_API_URL")}/email/send";
        var apiKey = Environment.GetEnvironmentVariable("SMTP2GO_API_KEY");

        var emailData = new
        {
            api_key = apiKey,
            sender = "esteban.villantoy@touchconsulting.pe",
            to = emails,
            subject = "Test Email",
            html_body = "<h1>Mensaje de Test Pilot Azure Functions</h1>"
        };

        var json = JsonSerializer.Serialize(emailData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await Client.PostAsync(urlApi, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = $"URL API {response.StatusCode}";
                throw new Exception(errorMessage);
            }
        }
        catch (Exception e)
        {
            throw new Exception($"Smtp2GoService:SendMails: {e}");
        }
    }
}