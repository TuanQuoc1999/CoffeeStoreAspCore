using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoffeeStoreAspCore.ViewModels.Catalog.Vouchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeStoreAspCore.AdminApp.Controllers
{
    [Authorize]
    public class VoucherController : Controller
    {
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<VoucherViewModel> List = new List<VoucherViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Voucher/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<VoucherViewModel>>(apiResponse);
                }
            }
            return View(List);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(VoucherCreateRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync
            ("https://localhost:5001/api/Voucher/CreateNew", httpContent);

                return RedirectToAction("Index", "Voucher");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =
client.GetAsync("https://localhost:5001/api/Voucher/GetById/" + id).Result;
                string stringData = response.Content.
            ReadAsStringAsync().Result;
                var data = JsonConvert.
            DeserializeObject<VoucherUpdateRequest>(stringData);
                return View(data);
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(VoucherUpdateRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(request);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync
            ("https://localhost:5001/api/Voucher/Update",
            contentData);
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;


                return RedirectToAction("Index", "Voucher");
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete( int id, string Status)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(Status);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PatchAsync
            ("https://localhost:5001/api/Voucher/ChangeStatus/"+id+"/"+Status,
            contentData);
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;


                return RedirectToAction("Index", "Voucher");
            }
        }
    }
}