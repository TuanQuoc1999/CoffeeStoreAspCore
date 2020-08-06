using CoffeeStoreAspCore.Application.Common;

using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Utilities;
using CoffeeStoreAspCore.ViewModels.DrinkRepo;
using CoffeeStoreAspCore.ViewModels.DrinkImage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog;
using CoffeeStoreAspCore.ViewModels.Menu;

namespace CoffeeStoreAspCore.Application.Catalog.Drinks
{
    

    public class ManageDrinkService : IManageDrinkService
    {
        private readonly StoreDBContext _context;
        private readonly IStorageService _storageService;
        public ManageDrinkService(StoreDBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> AddImage(int IdDrink, DrinkImageCreateRequest request)
        {
            var drinkImage = new DrinkImage()
            {
                Caption = request.Caption,
                IsDefault = request.IsDefault,
                IdDrink = IdDrink
                     
            };

            if (request.ImageFile != null)
            {
                drinkImage.ImagePath = await this.SaveFile(request.ImageFile);
                drinkImage.FileSize = request.ImageFile.Length;
            }
            _context.DrinkImages.Add(drinkImage);
            await _context.SaveChangesAsync();
            return drinkImage.Id;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
        public async Task<int> CreateDrink(DrinkCreateRequest request)
        {
            var drink = new Drink()
            {
                Name = request.Name,
                UnitPrice= request.UnitPrice,
                IdMenu=request.IdMenu
                
                                
            };
            //Save image
            if (request.ThumbnailImage != null)
            {
                drink.DrinkImage =   new DrinkImage()
                    {
                        Caption = "Thumbnail image",
                        ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        
                        FileSize = request.ThumbnailImage.Length
                };
             
            }
            _context.Drinks.Add(drink);
            
            await _context.SaveChangesAsync();
            return drink.Id;
        }


        public async Task<DrinkViewModel> GetById(int productId)
        {
            var drink = await _context.Drinks.Include(x=>x.DrinkImage).FirstOrDefaultAsync(x=>x.Id==productId);
            var DrinktViewModel = new DrinkViewModel()
            {
                Id = drink.Id,
                Name = drink.Name,
                UnitPrice = drink.UnitPrice,
                IdMenu=drink.IdMenu ,
                DrinkImage= new DrinkImageViewModel()
                {
                    Id= drink.DrinkImage.Id,
                    Caption=drink.DrinkImage.Caption,
                    ImagePath=drink.DrinkImage.ImagePath,
                    FileSize = drink.DrinkImage.FileSize,
                    IsDefault = drink.DrinkImage.IsDefault
                }
                
            };
            return DrinktViewModel;
        }

        public async Task<DrinkImageViewModel> GetImageById(int imageId)
        {

            var image = await _context.DrinkImages.FindAsync(imageId);
            if (image == null)
                throw new StoreException($"Cannot find an image with id {imageId}");

            var viewModel = new DrinkImageViewModel()
            {
                Caption = image.Caption,       
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
               
                FileSize = image.FileSize
         
            };
            return viewModel;
        }     
       
        public async Task<int> UpdateImage(int imageId, DrinkImageUpdateRequest request)
        {
            var productImage = await _context.DrinkImages.FindAsync(imageId);
            if (productImage == null)
                throw new StoreException($"Cannot find an image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.Caption = request.Caption;
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }
            _context.DrinkImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, int newPrice)
        {
            var drink = await _context.Drinks.FindAsync(productId);
            if (drink == null) throw new StoreException($"Cannot find a product with id: {productId}");
            drink.UnitPrice = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> UpdateDrink(DrinkUpdateRequest request)
        {
            var drink = await _context.Drinks.FindAsync(request.Id);
            if(drink == null) throw new StoreException($"Cannot find a product");
            drink.Name = request.Name;
            drink.IdMenu = request.IdMenu;
            drink.UnitPrice = request.UnitPrice;
            
            //Save image
            if (request.ThumbnailImage != null)
            {
                var thumbnailImage = await _context.DrinkImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.IdDrink == request.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbnailImage.Length;
                    thumbnailImage.ImagePath = await this.SaveFile(request.ThumbnailImage);
                    _context.DrinkImages.Update(thumbnailImage);
                }
            }
            _context.Drinks.Update(drink);
       return  await   _context.SaveChangesAsync();
           
        }

        public async Task<List<DrinkViewModel>> GetAll()
        {
            var query = from d in _context.Drinks
                        join m in _context.Menus on d.IdMenu equals m.Id
                        join i in _context.DrinkImages on d.Id equals i.IdDrink
                        where (d.Status == Status.Active)
                        select new { d ,i,m};
            var data = await query.Select(x => new DrinkViewModel()
            {
                Id = x.d.Id,
                Name = x.d.Name,
                IdMenu = x.d.IdMenu,
                UnitPrice = x.d.UnitPrice,
                NameMenu =x.m.Name,
                DrinkImage= new DrinkImageViewModel()
                {
                    Id = x.i.Id,
                    Caption = x.i.Caption,
                    ImagePath = x.i.ImagePath,
                    FileSize = x.i.FileSize,
                    IsDefault = x.i.IsDefault
                }

            }).ToListAsync();
            return data;

        }

        public async Task<List<DrinkViewModel>> GetAllDrinkByMenuId(int idMenu)
        {
            var query = from d in _context.Drinks
                        join i in _context.DrinkImages on d.Id equals i.IdDrink
                        where (d.Status == Status.Active && d.IdMenu== idMenu)
                        select new { d ,i};
            var data = await query.Select(x => new DrinkViewModel()
            {
                Id = x.d.Id,
                Name = x.d.Name,
                IdMenu = x.d.IdMenu,
                UnitPrice = x.d.UnitPrice,
                 DrinkImage = new DrinkImageViewModel()
                 {
                     Id = x.i.Id,
                     Caption = x.i.Caption,
                     ImagePath = x.i.ImagePath,
                     FileSize = x.i.FileSize,
                     IsDefault = x.i.IsDefault
                 }
            }).ToListAsync();
            return data;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Drinks.FindAsync(productId);
            if (product == null) throw new StoreException($"Cannot find a drink: {productId}");

            var images = _context.DrinkImages.Where(i => i.IdDrink == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Drinks.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> changeStatus(int productId, Status status)
        {
            var drink = await _context.Drinks.FindAsync(productId);
            if (drink == null) throw new StoreException($"Cannot find ");
            drink.Status = status;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<DrinkViewModel>> GetTop8NewDrink()
        {
            var  drinks = _context.Drinks.Where(x=>x.Status==Status.Active).Include(x=>x.DrinkImage).OrderByDescending(x=>x.Id).Take(8);
            var data = await drinks.Select(x => new DrinkViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                IdMenu = x.IdMenu,
                UnitPrice = x.UnitPrice,
                DrinkImage = new DrinkImageViewModel()
                {
                    Id = x.DrinkImage.Id,
                    Caption = x.DrinkImage.Caption,
                    ImagePath = x.DrinkImage.ImagePath,
                    FileSize = x.DrinkImage.FileSize,
                    IsDefault = x.DrinkImage.IsDefault
                }
            }).ToListAsync();
            return data;
        }
    }
}
