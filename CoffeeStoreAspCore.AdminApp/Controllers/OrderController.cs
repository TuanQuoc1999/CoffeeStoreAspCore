using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CoffeeStoreAspCore.ViewModels.Catalog.Bills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace CoffeeStoreAspCore.AdminApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;

        public OrderController( IHostingEnvironment hostingEnvironment)
        {
           
            _hostingEnvironment = hostingEnvironment;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            List<BillViewModel> List = new List<BillViewModel>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Order/GetAll"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<List<BillViewModel>>(apiResponse);
                }
                
            }
            return View(List);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(int id)
        {

            BillViewModel List = new BillViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Order/GetById/"+id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<BillViewModel>(apiResponse);
                }

            }
            return View(List);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Pending(int id, string Status)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(Status);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PatchAsync
            ("https://localhost:5001/api/Order/ChangeStatus/" + id + "/" + Status,
            contentData);
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;


                return RedirectToAction("Index");
            }
        }
        [Authorize(Roles = "Admin")]
      
        public async Task<IActionResult> ConfirmPayment(int id, string Status)
        {
            using (HttpClient client = new HttpClient())
            {
                string stringData = JsonConvert.SerializeObject(Status);
                var contentData = new StringContent(stringData,
            System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PatchAsync
            ("https://localhost:5001/api/Order/ChangeStatus/" + id + "/" + Status,
            contentData);
                ViewBag.Message = response.Content.
            ReadAsStringAsync().Result;


                return RedirectToAction("Index");
            }
        }


        public async Task<IActionResult> ExportExcel(int billId)
        {
            // Data Acces, load order header data.
            BillViewModel List = new BillViewModel();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:5001/api/Order/GetById/" + billId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    List = JsonConvert.DeserializeObject<BillViewModel>(apiResponse);
                }

            }
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $"Bill_{billId}.xlsx";
            // Template File
            string templateDocument = Path.Combine(sWebRootFolder, "templates", "BillTemplate.xlsx");

            string url = $"{Request.Scheme}://{Request.Host}/{"export-files"}/{sFileName}";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, "export-files", sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            using (FileStream templateDocumentStream = System.IO.File.OpenRead(templateDocument))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(templateDocumentStream))
                {

                    // add a new worksheet to the empty workbook
                    ExcelWorksheet worksheet = package.Workbook.Worksheets["Order"];

                    worksheet.Cells[4, 1].Value = "Order Date: "+List.Days;

                    // Insert customer data into template
                    worksheet.Cells[5, 1].Value = "Customer Name:"+List.UserName;
                    worksheet.Cells[6, 1].Value = "Address: "+List.Address;

                    // Start Row for Detail Rows
                    int rowIndex = 9;

                    // load order details

                    int count = 1;
                    foreach (var orderDetail in List.BillDetails)
                    {
                        // Cell 1, Carton Count
                        worksheet.Cells[rowIndex, 1].Value = count.ToString();
                        // Cell 2, Order Number (Outline around columns 2-7 make it look like 1 column)
                        worksheet.Cells[rowIndex, 2].Value = orderDetail.NameDrink;
                        // Cell 8, Weight in LBS (convert KG to LBS, and rounding to whole number)
                        worksheet.Cells[rowIndex, 3].Value = orderDetail.Quantity.ToString();

                        worksheet.Cells[rowIndex, 4].Value = orderDetail.Unitprice.ToString("N0");
                        worksheet.Cells[rowIndex, 5].Value = (orderDetail.Unitprice * orderDetail.Quantity).ToString("N0");
                        // Increment Row Counter
                        rowIndex++;
                        count++;
                    }
                    decimal total = (decimal)(List.ActualTotalAmount);
                    worksheet.Cells[26, 4].Value = total;
                    worksheet.Cells[24, 4].Value =  List.CalculatedTotalAmount;
                    worksheet.Cells[25, 4].Value =List.IdVoucher!=null?( List.DiscountPercent != null ? List.DiscountPercent * List.CalculatedTotalAmount / 100 : List.DiscountAmount):0;

                    worksheet.Cells.AutoFitColumns();

                    package.SaveAs(file); //Save the workbook.
                }
            }
            return Redirect(url);
        }
    }
}