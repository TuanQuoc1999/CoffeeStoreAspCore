using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoffeeStoreAspCore.ViewModels.Catalog;
using CoffeeStoreAspCore.ViewModels.DrinkRepo;
using CoffeeStoreAspCore.ViewModels.Menu;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeStoreAspCore.AdminApp.Controllers
{
    [Authorize]
    public class DrinkController : Controller
    {

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<MenuViewModel> LsMenu = new List<MenuViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var rep = await httpClient.GetAsync("https://localhost:5001/api/Menu/GetAll"))
                {
                    string apiRep = await rep.Content.ReadAsStringAsync();
                    LsMenu = JsonConvert.DeserializeObject<List<MenuViewModel>>(apiRep);
                }
                ViewBag.listMenu = LsMenu;
            }

            return View("Create");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(DrinkCreateRequest request)
        {

           using var httpClient = new HttpClient();
            {
                string boundary = $"{Guid.NewGuid().ToString()}";
                MultipartFormDataContent requestContent = new MultipartFormDataContent(boundary);
                requestContent.Headers.Remove("Content-Type");
                requestContent.Headers.TryAddWithoutValidation("Content-Type", $"multipart/form-data; boundary=--{boundary}");

                StreamContent streamContent = new StreamContent(request.ThumbnailImage.OpenReadStream());
                requestContent.Add(streamContent, request.ThumbnailImage.Name, request.ThumbnailImage.FileName);
                streamContent.Headers.ContentDisposition.FileNameStar = "";
                
                var idmenu = JsonConvert.SerializeObject(request.IdMenu);
                var Name = JsonConvert.SerializeObject(request.Name);
                var UnitPrice = JsonConvert.SerializeObject(request.UnitPrice);


                requestContent.Add(new StringContent(idmenu), "IdMenu");
                requestContent.Add(new StringContent(Name), "Name");
                requestContent.Add(new StringContent(UnitPrice), "UnitPrice");
                    var response = await httpClient.PostAsync
                    ("https://localhost:5001/api/Drink/CreateNew", requestContent);
                    var token = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("Index", "Drink");
            }
         }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            List<MenuViewModel> LsMenu = new List<MenuViewModel>();

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =
client.GetAsync("https://localhost:5001/api/Drink/GetById/" + id).Result;
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                var data = JsonConvert.
            DeserializeObject<DrinkUpdateRequest>(stringData);
                using (var rep = await client.GetAsync("https://localhost:5001/api/Menu/GetAll"))
                {
                    string apiRep = await rep.Content.ReadAsStringAsync();
                    LsMenu = JsonConvert.DeserializeObject<List<MenuViewModel>>(apiRep);
                }

                ViewBag.listMenu = LsMenu;
                return View(data);

            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(DrinkUpdateRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync
            ("https://localhost:5001/api/Drink/Update",
            contentData);
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;


                return RedirectToAction("Index", "Drink");
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id, string Status)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(Status);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PatchAsync
            ("https://localhost:5001/api/Drink/ChangeStatus/" + id + "/" + Status,
            contentData);
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;


                return RedirectToAction("Index", "Drink");
            }
        }
    }
}