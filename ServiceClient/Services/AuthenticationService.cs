using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceClient.Infrastructure;
using ServiceClient.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClient.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IOptions<AuthenticationSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthenticationService> _logger;

        private readonly string _remoteServiceBaseUrl;

        public AuthenticationService(HttpClient httpClient, ILogger<AuthenticationService> logger, IOptions<AuthenticationSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OauthURL}/oauth/";
        }

        public async Task<AuthenticationResponse> GetTokenAsync()
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(_settings.Value.ClientID + ":" + _settings.Value.ClientSecret));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", token);
            var uri = API.Authentication.GetTokenAsync(_remoteServiceBaseUrl);
            var response = await _httpClient.GetAsync(uri);
            var responseString = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<AuthenticationResponse>(responseString);
                return result;
            }
            else
            {
                var result = JsonConvert.DeserializeObject<AuthenticationError>(responseString);
                throw new Exception(result.Error);
            }
        }
    }
}
