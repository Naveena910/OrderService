using Entities;
using Entities.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Services.Client
{
    public class ProductClient
    {
        private readonly IHttpClientFactory httpClient;

        public ProductClient(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
        }

        public void GetAddress(Guid userId,Guid addressId,string token)
        {
            HttpClient client= httpClient.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5159");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.GetAsync($"api/address/{userId}/{addressId}").Result;

            if (!response.IsSuccessStatusCode) 
            {
                throw new NotFoundException("No user found with this id");
            }
        }
        public void GetPayment(Guid userId,Guid paymentId,string token)
        {
            HttpClient client = httpClient.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5159");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.GetAsync($"api/payment/{userId}/{paymentId}").Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException("No user found with this id");
            }
        }
        public ProductDto GetProductId(Guid? productId,string token)
        {
            HttpClient client = httpClient.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5288");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var items = client.GetFromJsonAsync<ProductDto>($"api/products/{productId}").Result;

            if(items==null)
            {
                throw new NotFoundException("No user found with this id");
            }
            return items;

        }
        public void DeleteCart(Guid? userId, string token)
        {
            HttpClient client = httpClient.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5288");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            HttpResponseMessage response = client.DeleteAsync($"api/cart/delete/{userId}").Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException("No user found with this id");
            }

        }

        public  IEnumerable<CartDto> GetCartItemsAsync(Guid userId, string token)
        {
            HttpClient client = httpClient.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5288");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var items =  client.GetFromJsonAsync<IEnumerable<CartDto>>($"api/cart/{userId}").Result;
            return items;
        }
    }
}
