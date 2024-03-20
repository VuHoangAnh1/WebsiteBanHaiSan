using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebBanHangOnline.Service
{
    public class CommonService
    {
        Uri BaseAddress = new Uri("https://localhost:44375/api");
        public readonly HttpClient _client;

        public CommonService()
        {
            _client = new HttpClient();
            _client.BaseAddress = BaseAddress;
        }
    }
}