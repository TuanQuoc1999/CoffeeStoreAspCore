using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeStoreAspCore.ViewModels.Catalog;
using CoffeeStoreAspCore.ViewModels.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeStoreAspCore.Controllers
{
    [Route("menu")]
    public class MenuController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MenuController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }




        [Route("index")]
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            List<MenuViewModel> LsMenu = new List<MenuViewModel>();
            List<DrinkViewModel> List = new List<DrinkViewModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Drink/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<DrinkViewModel>>(apiResponse);
                }

            }
            return View(List); 
        }

    }
}