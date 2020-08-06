using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Entities
{
  public  class DrinkImage
    {
        public int Id { get; set; }

        public int IdDrink { get; set; }

        public string Caption { get; set; }

        public string ImagePath { get; set; }

        public bool IsDefault { get; set; }

        public long FileSize { get; set; }
        public Drink Drink { get; set; }
    }
}
