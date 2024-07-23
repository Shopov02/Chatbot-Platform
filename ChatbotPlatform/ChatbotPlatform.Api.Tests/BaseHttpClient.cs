using System.Text;
using System.Net.Http.Headers;

using Newtonsoft.Json;

using ChatbotPlatform.Api.Tests.IdentityTests.ResponseModels;

namespace ChatbotPlatform.Api.Tests
{
    public class BaseHttpClient
    {
        private readonly HttpClient httpClient;

        public BaseHttpClient()
        {
            this.httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Constants.ApiBaseAddress),
            };
        }

        public async Task AuthorizeUser(string email, string password)
        {
            var loginContent = this.CreateJsonContent(new
            {
                Email = email,
                Password = password
            });

            var loginResponse = await this.PostAsync<LoginResponse>(
                Constants.IndentityApi.Login, 
                loginContent);

            this.httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", loginResponse.Token);
        }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
            => this.httpClient.PostAsync(requestUri, content);

        public async Task<T> PostAsync<T>(string requestUri, HttpContent content)
        {
            var response = await this.httpClient.PostAsync(requestUri, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(responseBody);

            return result;
        }

        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
            => this.httpClient.PutAsync(requestUri, content);

        public Task<HttpResponseMessage> HeadAsync(string requestUri)
            => this.httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, requestUri));

        public StringContent CreateJsonContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }
}