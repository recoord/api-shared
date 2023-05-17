using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Net.Http.Headers;

public class RecordingsApiV1HttpImpl : IRecordingsApiV1
{
    private readonly HttpClient _httpClient;

    public RecordingsApiV1HttpImpl(HttpClient httpClient)
    {
        _httpClient = httpClient;
        var url = Environment.GetEnvironmentVariable("RECORDINGS_API_URL")!;
        _httpClient.BaseAddress = new Uri(url);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        var username = Environment.GetEnvironmentVariable("API_USERNAME")!;
        var password = Environment.GetEnvironmentVariable("API_PASSWORD")!;
        var authenticationString = $"{username}:{password}";
        var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
        _httpClient.DefaultRequestHeaders.Remove(HeaderNames.Authorization);
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Basic {base64EncodedAuthenticationString}");
    }

    public async Task<RecordingGetResponseV1> RecordingGet(RecordingGetRequestV1 recordingGetRequest)
    {
        var response = await _httpClient.GetAsync($"{recordingGetRequest.RecordingId}");
        var result = response.Content.ReadAsStringAsync().Result;

        if (response.StatusCode.IsSuccessStatusCode())
        {
            return JsonSerializer.Deserialize<RecordingGetResponseV1>(result, new JsonSerializerOptions(JsonSerializerDefaults.Web))!;
        }
        else
        {
            throw new InvalidOperationException($"Error getting recording for {nameof(recordingGetRequest.RecordingId)}={recordingGetRequest.RecordingId}");
        }
    }
}