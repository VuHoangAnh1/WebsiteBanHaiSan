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
    public class ProductImageService : CommonService
    {
        public List<ProductImage> GetProductImages()
        {
            List<ProductImage> categoriesList = new List<ProductImage>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ProductImages").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categoriesList = JsonConvert.DeserializeObject<List<ProductImage>>(data);
            }
            return categoriesList;
        }

        public ProductImage GetProductImages(int idProduct)
        {
            ProductImage ProductImages = new ProductImage();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ProductImages/" + idProduct).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ProductImages = JsonConvert.DeserializeObject<ProductImage>(data);
            }
            return ProductImages;
        }

        public HttpResponseMessage PostProductImage(ProductImage model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/ProductImages/PostProductImage", content).Result;
            return response;
        }

        public HttpResponseMessage PutProductImage(ProductImage model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/ProductImages/", content).Result;
            return response;
        }

        public HttpResponseMessage DeleteProductImage(int id)
        {
            HttpResponseMessage responce = _client.DeleteAsync(_client.BaseAddress + "/ProductImages/" + id).Result;
            return responce;
        }

        public List<ProductImage> GetProductImagesByProductId(int productId) 
        {
            List<ProductImage> categoriesList = new List<ProductImage>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/productImages/"+productId+"/GetProductImageByProductId").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                categoriesList = JsonConvert.DeserializeObject<List<ProductImage>>(data);
            }
            return categoriesList;
        }
    }
}