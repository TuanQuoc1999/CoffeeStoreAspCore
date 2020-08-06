using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.DrinkImage
{
  public  class DrinkImageViewModel
    {
        public int Id { get; set; }

       

        public string Caption { get; set; }

        public string ImagePath { get; set; }

        public bool IsDefault { get; set; }

        public long FileSize { get; set; }
    }
}
