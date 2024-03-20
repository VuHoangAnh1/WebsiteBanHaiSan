using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Service
{
    public class ProductCategoryService : CommonService
    {
        public List<ProductCategory> GetProductCategories()
        {
            List<ProductCategory> categoriesList = new List<ProductCategory>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ProductCategories").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categoriesList = JsonConvert.DeserializeObject<List<ProductCategory>>(data);
            }
            return categoriesList;
        }

        public ProductCategory GetProductCategory(int id)
        {
            ProductCategory ProductCategories = new ProductCategory();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ProductCategories/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ProductCategories = JsonConvert.DeserializeObject<ProductCategory>(data);
            }
            return ProductCategories;
        }

        public HttpResponseMessage PostProductCategory(ProductCategory model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/ProductCategories/PostCategory", content).Result;
            return response;
        }

        public HttpResponseMessage PutProductCategory(ProductCategory model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/ProductCategories/", content).Result;
            return response;
        }

        public HttpResponseMessage DeleteProductCategory(int id)
        {
            HttpResponseMessage responce = _client.DeleteAsync(_client.BaseAddress + "/ProductCategories/" + id).Result;
            return responce;
        }
    }
}