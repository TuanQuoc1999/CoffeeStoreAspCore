using CoffeeStoreAspCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Models
{
    public class Item
    {
        public Drink Drink { get; set; }

        public int Quantity { get; set; }
    }
}
