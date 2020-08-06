using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog
{
   public class DrinkCreateRequest
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int IdMenu { get; set; }
    
        public IFormFile ThumbnailImage { get; set; }
    }
}
