using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chat_Client.Core.Tools;

internal static class DataProvider
{
    private static readonly string _host = Config.GetValue("host");

    private static readonly HttpClient _client = new();

    private static readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    internal static async Task<T> GetAsync<T>(string controller, string path)
    {
        var requestString = _host + "chat/" + controller + path;

        var response = await _client.GetAsync(requestString);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<T>(content, _serializerOptions)!;

        return result;
    }

    internal static async Task<HttpStatusCode> PostAsync<T>(T value, string controller)
    {
        var requestString = _host + "chat/" + controller;

        var json = JsonSerializer.Serialize(value);

        var answer = await _client.PostAsync(requestString, new StringContent(json, Encoding.UTF8, "application/json"));

        return answer.StatusCode;
    }

    internal static async Task<HttpStatusCode> DeleteAsync(int id, string controller)
    {
        var requestString = _host + "chat/" + controller + $"/{id}";

        var answer = await _client.DeleteAsync(requestString);

        return answer.StatusCode;
    }
}