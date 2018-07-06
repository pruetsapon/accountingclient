using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WS.Model;
using WS.Services;
using System.Threading;
using System.Net;

namespace WS.Client
{   
    public class Client : IClient
    {
        private string _baseUrl { get; }
        private HttpClient _httpClient { get; set; }
        public string _tokenKey { get; set; }
        public Client(string baseUrl)
        {
            this._baseUrl =  baseUrl;
            this._httpClient = this.getWSClient();
        }

        public void SetAccessToken(string token)
        {
            this._httpClient = this.GetWSClient(token);
        }

        public CustomerService Customer
        { 
            get
            {
                return new CustomerService(this._httpClient);
            }
        }

        public async Task<string> GetAccessToken(string tokenKey)
        {
            HttpResponseMessage response = await this._httpClient.PostAsJsonAsync("api/site/login", tokenKey);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsAsync<string>();
                return token;
            }
            return null;
        }

        ///  Get HttpClient object
        private HttpClient getWSClient()
        {
            var client = new HttpClient(new AuthenHandler(new HttpClientHandler(), this));
            client.BaseAddress = new Uri(this._baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        ///  Get HttpClient object with access token
        private HttpClient GetWSClient(string token)
        {
            var client = new HttpClient(new AuthenHandler(new HttpClientHandler(), this));
            client.BaseAddress = new Uri(this._baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", token
            );
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }

    public class AuthenHandler : DelegatingHandler
    {
        private Client _wsClient;
        public AuthenHandler(HttpMessageHandler innerHandler, Client client)
            : base(innerHandler)
        {
            _wsClient = client;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            response = await base.SendAsync(request, cancellationToken);
            if(response.IsSuccessStatusCode)
            {
                return response;
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var token = _wsClient.GetAccessToken(_wsClient._tokenKey).Result;
                if(token != null)
                {
                    _wsClient.SetAccessToken(token);
                    request.Headers.Add("Authorization", "Bearer " + token);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }
            return response;
        }
    }

    public interface IClient
    {
        CustomerService Customer { get; }
    }
}
