using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoffeeStoreAspCore.ViewModels.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeStoreAspCore.AdminApp.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index ()
        {

            List<MenuViewModel> List = new List<MenuViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Menu/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<MenuViewModel>>(apiResponse);
                }
            }
            return View(List);


        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public  ActionResult Create()
        {
            return View("Create");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit( int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =
client.GetAsync("https://localhost:5001/api/Menu/GetById/"+id).Result;
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                var data = JsonConvert.
            DeserializeObject<MenuUpdateRequest>(stringData);
                return View(data);
            }
          
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(MenuCreateRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync
            ("https://localhost:5001/api/Menu/CreateNew", httpContent);
                
                 return RedirectToAction("Index","Menu");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public  async Task<ActionResult> Edit(MenuUpdateRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync
            ("https://localhost:5001/api/Menu/Update",
            contentData);
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;
               

                return RedirectToAction("Index", "Menu");
            }
        }
    }
}