using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WS.Model;

namespace WS.Services
{
    public class CustomerService
    {
        private HttpClient WSClient { get; set; }
        public CustomerService(HttpClient wsClient)
        {
            this.WSClient = wsClient;
        }

        // get all customer
        public async Task<List<Customer>> Get()
        {
            HttpResponseMessage response = await this.WSClient.GetAsync("api/customer");
            if (response.IsSuccessStatusCode)
            {
                var customers = await response.Content.ReadAsAsync<List<Customer>>();
                return customers;
            }
            return null;
        }

        // get customer by id
        public async Task<Customer> Get(int id)
        {
            HttpResponseMessage response = await this.WSClient.GetAsync("api/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var customer = await response.Content.ReadAsAsync<Customer>();
                return customer;
            }
            return null;
        }

        // create customer
        public async Task<Customer> Create(Customer customer)
        {
            HttpResponseMessage response = await this.WSClient.PostAsJsonAsync("api/customer/", customer);
            if (response.IsSuccessStatusCode)
            {
                var created = await response.Content.ReadAsAsync<Customer>();
                return created;
            }
            return null;
        }

        // update customer by id
        public async Task<Customer> Update(int id, Customer customer)
        {
            HttpResponseMessage response = await this.WSClient.PutAsJsonAsync("api/customer/" + id, customer);
            if (response.IsSuccessStatusCode)
            {
                var updated = await response.Content.ReadAsAsync<Customer>();
                return updated;
            }
            return null;
        }

        // delete customer by id
        public async Task<Customer> Delete(int id)
        {
            HttpResponseMessage response = await this.WSClient.DeleteAsync("api/customer/" + id);
            if (response.IsSuccessStatusCode)
            {
                var deleted = await response.Content.ReadAsAsync<Customer>();
                return deleted;
            }
            return null;
        }
    }
}
