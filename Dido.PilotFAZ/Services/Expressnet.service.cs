using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dido.PilotFAZ.Services;

public static class ExpressnetService
{
    private static readonly HttpClient Client = new HttpClient();

    public static async Task<string> FetchUserData()
    {
        var urlApi = Environment.GetEnvironmentVariable("API_EXPRESSNET");

        try
        {
            var response = await Client.GetAsync(urlApi);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = $"URL API {response.StatusCode}";
                throw new Exception(errorMessage);
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }
        catch (Exception e)
        {
            throw new Exception($"GetUsers: {e}");
        }
    }
}