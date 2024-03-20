using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Service;

public class NewsService : CommonService
{
    public List<News> GetNews()
    {
        List<News> categoriesList = new List<News>();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/News").Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            categoriesList = JsonConvert.DeserializeObject<List<News>>(data);
        }
        return categoriesList;
    }

    public News GetNews(int id)
    {
        News news = new News();
        HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/News/" + id).Result;
        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            news = JsonConvert.DeserializeObject<News>(data);
        }
        return news;
    }

    public HttpResponseMessage PostNews(News model)
    {
        string data = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/News/PostNews", content).Result;
        return response;
    }

    public HttpResponseMessage PutNews(News model)
    {
        string data = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/News/", content).Result;
        return response;
    }

    public HttpResponseMessage DeleteNews(int id)
    {
        HttpResponseMessage responce = _client.DeleteAsync(_client.BaseAddress + "/News/" + id).Result;
        return responce;
    }
}