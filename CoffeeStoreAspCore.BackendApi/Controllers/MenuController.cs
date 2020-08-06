using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Application.Catalog.Drinks;
using CoffeeStoreAspCore.Application.Catalog.Menus;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreAspCore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IManageMenuService _manageMenuService;
        
        public MenuController(
            IManageMenuService manageMenuService)
        {

            _manageMenuService = manageMenuService;
         
        }
        [HttpPost("CreateNew")]
        public async Task<IActionResult> Create( MenuCreateRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var MenuId = await _manageMenuService.Create(request);
            if (MenuId == 0)
                return BadRequest();

            var menu = await _manageMenuService.GetById(MenuId);

            return CreatedAtAction(nameof(GetById), new { id = MenuId }, menu);
        }
        [HttpGet("GetById/{MenuId}")]
        public async Task<IActionResult> GetById( int MenuId)
        {
            var menu = await _manageMenuService.GetById(MenuId);
            if (menu == null)
                return BadRequest("Cannot find product");
            return Ok(menu);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var menus = await _manageMenuService.GetAllMenu();
         
            return Ok(menus);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update( MenuUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var menu = await _manageMenuService.Update(request);
            if (menu == 0)
                return BadRequest();
            return Ok();
        }
        [HttpDelete("Delete/{menuId}")]
        public async Task<IActionResult> Delete(int menuId)
        {
            var affectedResult = await _manageMenuService.Delete(menuId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

       
    }
}