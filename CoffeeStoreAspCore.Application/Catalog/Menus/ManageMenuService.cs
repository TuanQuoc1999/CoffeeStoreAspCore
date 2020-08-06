using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Utilities;
using CoffeeStoreAspCore.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.DrinkRepo;
using CoffeeStoreAspCore.ViewModels.DrinkImage;
using CoffeeStoreAspCore.ViewModels.Catalog;

namespace CoffeeStoreAspCore.Application.Catalog.Menus
{
   public class ManageMenuService : IManageMenuService
    {
            private readonly StoreDBContext _context;

            public ManageMenuService(StoreDBContext context)
            {
                _context = context;

            }
        public async Task<int> Create(MenuCreateRequest request)
        {

            var menu = new Menu()
            {
                Name = request.Name

            };
             _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu.Id;
        }

        public async Task<int> Delete(int menuId)
        {

            var menu = await _context.Menus.FindAsync(menuId);
            if (menu == null) throw new StoreException($"Cannot find a menu: {menuId}");

           
            _context.Menus.Remove(menu);

            return await _context.SaveChangesAsync();
        }

        
        public async Task<List<MenuViewModel>> GetAllMenu()
        {
            var menus = await _context.Menus.Where(x => x.Status == Status.Active).Include(x => x.Drinks).ThenInclude(x => x.DrinkImage).ToListAsync();
            var data = menus.Select(x => new MenuViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                drinks = x.Drinks.Select(d => new DrinkViewModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    UnitPrice = d.UnitPrice,
                    DrinkImage = new DrinkImageViewModel()
                    {
                        Id = d.DrinkImage.Id,
                        Caption = d.DrinkImage.Caption,
                        ImagePath = d.DrinkImage.ImagePath,
                        FileSize = d.DrinkImage.FileSize,
                        IsDefault = d.DrinkImage.IsDefault
                    }
                }).ToList()
            }).ToList();

            return data;


        }

        public async Task<MenuViewModel> GetById(int IdMenu)
        {
            var menu = await _context.Menus.Where(x => x.Id == IdMenu).Include(x => x.Drinks).ThenInclude(x => x.DrinkImage).FirstOrDefaultAsync();
            var MenuViewModel = new MenuViewModel()
            {
                Id = menu.Id,
                Name = menu.Name,
                drinks = menu.Drinks.Select(d => new DrinkViewModel()
                {
                    Id = d.Id,
                    Name = d.Name,
                    UnitPrice = d.UnitPrice,
                   
                    DrinkImage = new DrinkImageViewModel()
                    {
                        Id = d.DrinkImage.Id,
                        Caption = d.DrinkImage.Caption,
                        ImagePath = d.DrinkImage.ImagePath,
                        FileSize = d.DrinkImage.FileSize,
                        IsDefault = d.DrinkImage.IsDefault
                    }
                }).ToList()

            };
            return MenuViewModel;

        }

        public async Task<int> Update(MenuUpdateRequest request)
        {
            var menu = await _context.Menus.FindAsync(request.Id);
            if (menu == null) throw new StoreException($"Cannot find ");
           
                menu.Name = request.Name;

            return await _context.SaveChangesAsync();
             
        }
    }
}
