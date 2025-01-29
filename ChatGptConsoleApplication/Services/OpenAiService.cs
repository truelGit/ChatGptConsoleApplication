using System.Text;
using Newtonsoft.Json;

namespace ChatGptConsoleApplication.Services
{
    public class OpenAiService
    {
        private readonly HttpClient _httpClient;
        public OpenAiService(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        }

        public async Task<string> GetResponseAsync(string userInput)
        {
            var requestUri = "https://api.openai.com/v1/chat/completions";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = userInput }
                },
                max_tokens = 100
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(requestUri, jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"OpenAI API Error: {response.StatusCode} - {errorMessage}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}