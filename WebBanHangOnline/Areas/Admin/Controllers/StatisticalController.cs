using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Controllers;
using WebBanHangOnline.Models;
using static WebBanHangOnline.ApiControllers.StatisticalController;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatisticalController : Controller
    {
        private OrderService _orderService;
        // GET: Admin/Statistical
        public StatisticalController()
        {
            _orderService = new OrderService();
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Month(int? selectedYear)
        {
            try
            {
                // Kiểm tra nếu các tham số selectedYear và selectedMonth không null
                if (selectedYear.HasValue)
                {
                    // Gọi API bằng HttpClient
                    var apiEndpoint = $"https://localhost:44375/api/Statistical/Monthly/{selectedYear}";

                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetStringAsync(apiEndpoint);

                        // Deserializing JSON response to the model
                        var dataFromApi = JsonConvert.DeserializeObject < List <MonthlyRevenue>>(response);

                        // Truyền dữ liệu đến view
                        return Json(dataFromApi, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // Nếu có ít nhất một tham số là null, trả về dữ liệu mặc định hoặc thông báo lỗi tùy ý
                    return Json(null, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching data from the API: {ex.ToString()}");
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetStatistical()
        {
            var result = _orderService.GetStatisticals();
            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

    }
}