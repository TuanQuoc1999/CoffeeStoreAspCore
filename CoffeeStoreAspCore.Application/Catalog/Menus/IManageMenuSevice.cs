using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.ViewModels.Menu;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Application.Catalog.Menus
{
  public  interface IManageMenuService
    {
        
        Task<int> Create(MenuCreateRequest request);

        Task<int> Update(MenuUpdateRequest request);
      
        Task<MenuViewModel> GetById(int IdMenu);

        Task<List<MenuViewModel>> GetAllMenu();

        Task<int> Delete(int menuId);
    }
}
