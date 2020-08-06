using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Application.Catalog.Bills;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog.BillDeatils;
using CoffeeStoreAspCore.ViewModels.Catalog.Bills;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreAspCore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IManageBillService _manageBillService;
        public OrderController(
            IManageBillService manageBillService)
        {

            _manageBillService = manageBillService;
        }
        [HttpPost("CreateNew")]
      
        public async Task<IActionResult> CreateBill(BillCreateRequest billrequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var billId = await _manageBillService.CreateBill(billrequest);
            if (billId == 0)
                return BadRequest();

            var product = await _manageBillService.GetById(billId);

            return CreatedAtAction(nameof(GetById), new { id = billId }, product);
        }
        [HttpGet("GetById/{BillId}")]
        public async Task<IActionResult> GetById(int BillId)
        {
            var product = await _manageBillService.GetById(BillId);
            if (product == null)
                return BadRequest("Cannot find ");
            return Ok(product);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var oders = await _manageBillService.GetAll();

            return Ok(oders);
        }
        [HttpPatch("ChangeStatus/{idOrder}/{status}")]
        public async Task<IActionResult> ChangeStatus(int idOrder, OrderStatus status)
        {
            var isSuccessful = await _manageBillService.UpdateStatus(idOrder, status);
            if (isSuccessful)
                return Ok();

            return BadRequest();
        }
        [HttpGet("report/{fromdate}/{todate}")]
        public async Task<IActionResult> Report(DateTime fromdate, DateTime todate)
        {
            var report = await _manageBillService.Report(fromdate, todate);
            return Ok(report);
        }
        [HttpGet("reportproduct/{fromdate}/{todate}")]
        public async Task<IActionResult> ReportProduct(DateTime fromdate, DateTime todate)
        {
            var report = await _manageBillService.ReportProduct(fromdate, todate);
            return Ok(report);
        }
    }
}