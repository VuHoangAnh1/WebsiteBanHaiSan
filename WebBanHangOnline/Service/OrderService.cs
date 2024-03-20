using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Models.ViewModels;
using WebBanHangOnline.Service;

namespace WebBanHangOnline.Controllers
{
    public class OrderService : CommonService
    {
        public List<Order> GetOrders()
        {
            List<Order> ordersList = new List<Order>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ordersList = JsonConvert.DeserializeObject<List<Order>>(data);
            }
            return ordersList;
        }

        public Order GetOrder(int id)
        {
            Order Orders = new Order();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Orders = JsonConvert.DeserializeObject<Order>(data);
            }
            return Orders;
        }

        public HttpResponseMessage PostOrder(Order model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Orders/PostOrder", content).Result;
            return response;
        }

        public HttpResponseMessage PutOrders(Order model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Orders/", content).Result;
            return response;
        }

        public HttpResponseMessage DeleteOrder(int id)
        {
            HttpResponseMessage responce = _client.DeleteAsync(_client.BaseAddress + "/Orders/" + id).Result;
            return responce;
        }

        public List<OrderDetail> GetOrderDetails (int id) 
        {
            List<OrderDetail> orderDetailsList = new List<OrderDetail>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/orders/{id}/details").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                orderDetailsList = JsonConvert.DeserializeObject<List<OrderDetail>>(data);
            }
            return orderDetailsList;
        }

        public Order GetOrderByCode(string code)
        {
            Order order = new Order();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/orders/{code}/byCode").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                order = JsonConvert.DeserializeObject<Order>(data);
            }
            return order;
        }

        public List<RevenueStatisticViewModel> GetStatisticals()
        {
            List<RevenueStatisticViewModel> ordersList = new List<RevenueStatisticViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/Statiscal").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ordersList = JsonConvert.DeserializeObject<List<RevenueStatisticViewModel>>(data);
            }
            return ordersList;
        }
    }
}