using Fithub.UI.Interfaces;
using Fithub.UI.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fithub.UI.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IStateContainer _container;
        private readonly ILocalStorage _storage;

        public HttpService(HttpClient httpClient, IStateContainer container, ILocalStorage storage)
        {
            _httpClient = httpClient;
            _container = container;
            _storage = storage;
        }

        public async Task<T> Delete<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequest<T>(request);
        }

        public async Task<T> Get<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await SendRequest<T>(request);
        }

        public async Task<T> Post<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequest<T>(request);
        }

        public async Task<T> Put<T>(string uri, object value)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await SendRequest<T>(request);
        }

        private async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            // Add jwt auth header if user is logged in and request is to the api url
            var user = _container.GetItem<User>("user");
            var isApiUrl = !request.RequestUri.IsAbsoluteUri;

            if (user != null && isApiUrl)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);

            using var response = await _httpClient.SendAsync(request);

            // User is unathorized
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _container.RemoveItem("user");
                await _storage.RemoveItem("user");
            }

            // Throw exception on error response
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
