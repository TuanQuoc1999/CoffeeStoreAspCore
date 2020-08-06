using CoffeeStoreAspCore.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Entities
{
  public  class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public List<Drink> Drinks { get; set; }
    }
}
