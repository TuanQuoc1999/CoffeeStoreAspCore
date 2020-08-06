
using CoffeeStoreAspCore.ViewModels.DrinkImage;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog;

namespace CoffeeStoreAspCore.Application.Catalog.Drinks
{
   public interface IManageDrinkService
    {
        
        Task<int> CreateDrink(DrinkCreateRequest request);

        Task<int> UpdateDrink(DrinkUpdateRequest request);
        Task<int> Delete(int productId);
        Task<bool> UpdatePrice(int productId, int newPrice);
        Task<DrinkViewModel> GetById(int productId);
        Task<List<DrinkViewModel>> GetAll();

        Task<int> AddImage(int IdDrink, DrinkImageCreateRequest request);

        Task<int> UpdateImage(int imageId, DrinkImageUpdateRequest request);

        Task<DrinkImageViewModel> GetImageById(int imageId);
        Task<List<DrinkViewModel>> GetAllDrinkByMenuId(int idMenu);
        Task<bool> changeStatus(int productId, Status status);
        
        Task<List<DrinkViewModel>> GetTop8NewDrink();

        
    }
}
