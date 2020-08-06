
using CoffeeStoreAspCore.ViewModels.Catalog;
using CoffeeStoreAspCore.ViewModels.DrinkRepo;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Menu
{
    public class MenuViewModel
    {
        public int Id { get; set; }
        public string Name { set; get; }

        public List<DrinkViewModel> drinks { set; get; }



    }
}
