using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Application.Catalog.Drinks;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog;
using CoffeeStoreAspCore.ViewModels.DrinkImage;
using CoffeeStoreAspCore.ViewModels.DrinkRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeStoreAspCore.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   /* [Authorize]*/
    public class DrinkController : ControllerBase
    {
       // private readonly IPublicDrinkService _publicProductService;
        private readonly IManageDrinkService _manageDrinkService;
        public DrinkController(
            IManageDrinkService manageProductService)
        {
          
            _manageDrinkService = manageProductService;
        }
        [HttpPost ("CreateNew")]
        public async Task<IActionResult> Create([FromForm]DrinkCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageDrinkService.CreateDrink(request);
            if (productId == 0)
                return BadRequest();

            var product = await _manageDrinkService.GetById(productId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }
        [HttpGet("GetById/{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _manageDrinkService.GetById(productId);
            if (product == null)
                return BadRequest("Cannot find product");
            return Ok(product);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update(DrinkUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageDrinkService.UpdateDrink(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpPatch("GetByMenuId/{idMenu}")]
        public async Task<IActionResult> GetAllDrinkByMenuId(int idMenu)
        {
            var drinks = await _manageDrinkService.GetAllDrinkByMenuId(idMenu);
            return Ok(drinks);
        }

        [HttpPatch("UpdatePrice/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, int newPrice)
        {
            var isSuccessful = await _manageDrinkService.UpdatePrice(productId, newPrice);
            if (isSuccessful)
                return Ok();

            return BadRequest();
        }

        //Images
        [HttpPost("CreateImage/{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId,[FromForm]DrinkImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageDrinkService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _manageDrinkService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("UpdateImgage/{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId,DrinkImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manageDrinkService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }
        [HttpGet("GetImageById/{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _manageDrinkService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Cannot find product");
            return Ok(image);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var drinks = await _manageDrinkService.GetAll();
            return Ok(drinks);
        }
        [HttpDelete("Delete/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _manageDrinkService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
        [HttpPatch("ChangeStatus/{productId}/{status}")]
        public async Task<IActionResult> ChangeStatus(int productId, Status status)
        {
            var isSuccessful = await _manageDrinkService.changeStatus(productId, status);
            if (isSuccessful)
                return Ok();

            return BadRequest();
        }
        [HttpGet("GetTop")]
        public async Task<IActionResult> GetTop()
        {
            var drinks = await _manageDrinkService.GetTop8NewDrink(); 
            return Ok(drinks);
        }
    }
}