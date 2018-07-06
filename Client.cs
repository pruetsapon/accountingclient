using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WS.Model;
using WS.Services;

namespace WS.Client
{   
    public class Client : IClient
    {
        private string _baseUrl { get; }
        private HttpClient _httpClient { get; set; }
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
            var client = new HttpClient();
            client.BaseAddress = new Uri(this._baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        ///  Get HttpClient object with access token
        private HttpClient GetWSClient(string token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(this._baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", token
            );
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }

    public interface IClient
    {
        CustomerService Customer { get; }
    }
}
