using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.ViewModels.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoffeeStoreAspCore.AdminApp.Controllers
{
    public class ThongkeController : Controller
    {
      
        
        public async Task<IActionResult> Index(string fromdate = "0" , string todate="0" )
        {
            var now = DateTime.Now;
           
                var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
            var fromDate = fromdate == "0" ? firstDayOfMonth.ToString("yyyy-MM-dd") : fromdate;
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var toDate = todate == "0" ? lastDayOfMonth.ToString("yyyy-MM-dd") : todate;
            ViewBag.fromDate = fromDate;
            ViewBag.toDate = toDate;
            List<RevenueReportViewModel> List = new List<RevenueReportViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Order/Report/"+ fromDate + "/"+ toDate))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<RevenueReportViewModel>>(apiResponse);
                }

            }
            return View(List);
        }
       
    }
}