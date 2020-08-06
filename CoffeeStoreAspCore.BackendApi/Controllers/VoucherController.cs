using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Application.Catalog.Vouchers;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog.Vouchers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreAspCore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IManageVoucherService _manageVoucherService;
        public VoucherController(
            IManageVoucherService manageVoucherService)
        {

            _manageVoucherService = manageVoucherService;
        }
        [HttpPost("CreateNew")]
        public async Task<IActionResult> Create(VoucherCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var idVoucher = await _manageVoucherService.Create(request);
            if (idVoucher == 0)
                return BadRequest();

            var product = await _manageVoucherService.GetById(idVoucher);

            return CreatedAtAction(nameof(GetById), new { id = idVoucher }, product);
        }

        [HttpGet("GetById/{idVoucher}")]
        public async Task<IActionResult> GetById(int idVoucher)
        {
            var voucher = await _manageVoucherService.GetById(idVoucher);
            if (voucher == null)
                return BadRequest("Cannot find ");
            return Ok(voucher);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _manageVoucherService.GetAll();

            return Ok(contacts);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update( VoucherUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var voucher = await _manageVoucherService.Update(request);
            if (voucher == 0)
                return BadRequest();
            return Ok();
        }
        [HttpGet("GetByCode/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var voucher = await _manageVoucherService.GetByCode(code);
            if (voucher == null)
                return BadRequest("Cannot find ");
            return Ok(voucher);
        }
        [HttpPatch("ChangeStatus/{voucherId}/{status}")]
        public async Task<IActionResult> ChangeStatus(int voucherId, Status status)
        {
            var isSuccessful = await _manageVoucherService.ChangeStatus(voucherId, status);
            if (isSuccessful)
                return Ok();

            return BadRequest();
        }
    }
}