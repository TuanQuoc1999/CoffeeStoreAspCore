
using CoffeeStoreAspCore.ViewModels.DrinkImage;
using CoffeeStoreAspCore.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog
{
 public   class DrinkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int IdMenu { get; set; }

        public string NameMenu { get; set; }
        public DrinkImageViewModel DrinkImage { get; set; }
    }
}
