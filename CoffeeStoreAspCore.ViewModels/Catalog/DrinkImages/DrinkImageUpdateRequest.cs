using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.DrinkImage
{
  public  class DrinkImageUpdateRequest
    {
       
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
