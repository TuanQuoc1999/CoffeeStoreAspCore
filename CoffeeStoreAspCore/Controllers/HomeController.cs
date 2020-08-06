using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeStoreAspCore.Models;

using System.Net.Http;

using CoffeeStoreAspCore.ViewModels.Menu;
using CoffeeStoreAspCore.ViewModels.Catalog;
using Newtonsoft.Json;

namespace CoffeeStoreAspCore.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<MenuViewModel> LsMenu = new List<MenuViewModel>();
            List<DrinkViewModel> List = new List<DrinkViewModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5000/api/Drink/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<DrinkViewModel>>(apiResponse);
                }

            }
            return View(List);

        }



       
    }
}
