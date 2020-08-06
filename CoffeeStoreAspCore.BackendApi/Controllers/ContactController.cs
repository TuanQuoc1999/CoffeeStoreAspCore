using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Application.Catalog.Contacts;
using CoffeeStoreAspCore.ViewModels.Catalog.Contacts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreAspCore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IManagerContacService _manageContactService;
        public ContactController(
            IManagerContacService manageContactService)
        {

            _manageContactService = manageContactService;
        }
        [HttpPost("CreateNew")]
        public async Task<IActionResult> Create([FromForm]ContactCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var idContact = await _manageContactService.Create(request);
            if (idContact == 0)
                return BadRequest();

            var product = await _manageContactService.GetById(idContact);

            return CreatedAtAction(nameof(GetById), new { id = idContact }, product);
        }

        [HttpGet("GetById/{idContact}")]
        public async Task<IActionResult> GetById(int idContact)
        {
            var contact = await _manageContactService.GetById(idContact);
            if (contact == null)
                return BadRequest("Cannot find ");
            return Ok(contact);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _manageContactService.GetAll();
         
            return Ok(contacts);
        }
    }
}