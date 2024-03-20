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
    public class CategoriesService : CommonService
    {
        public List<Category> GetCategories()
        {
            List<Category> categoriesList = new List<Category>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/categories").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categoriesList = JsonConvert.DeserializeObject<List<Category>>(data);
            }
            return categoriesList;
        }

        public Category GetCategory(int id)
        {
            Category categories = new Category();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/categories/"+id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<Category>(data);
            }
            return categories;
        }

        public HttpResponseMessage PostCategory(Category model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Categories/PostCategory", content).Result;
            return response;
        }

        public HttpResponseMessage PutCategory(Category model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Categories/", content).Result;
            return response;
        }

        public HttpResponseMessage DeleteCategory(int id)
        {
            HttpResponseMessage responce = _client.DeleteAsync(_client.BaseAddress + "/Categories/" + id).Result;
            return responce;
        }
    }
}