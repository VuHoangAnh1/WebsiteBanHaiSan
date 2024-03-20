using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Service
{
    public class ProductService : CommonService
    {
        public List<Product> GetProducts()
        {
            List<Product> productsList = new List<Product>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/products").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productsList = JsonConvert.DeserializeObject<List<Product>>(data);
            }
            return productsList;
        }
        public Product GetProduct(int id)
        {
            Product product = new Product();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/products/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(data);
            }
            return product;
        }
        public HttpResponseMessage PutProduct(Product model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Products/", content).Result;
            return response;
        }

        public HttpResponseMessage PostProduct(Product model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Products/", content).Result;
            return response;
        }
        public HttpResponseMessage DeleteProduct(int id)
        {
            var response = _client.DeleteAsync(_client.BaseAddress + "/Products/" + id).Result;
            return response;
        }
    } 
}