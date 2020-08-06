using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeStoreAspCore.ViewModels.Catalog.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeStoreAspCore.AdminApp.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<ContactViewModel> List = new List<ContactViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Contact/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<ContactViewModel>>(apiResponse);
                }
            }
            return View(List);
        }
    }
}