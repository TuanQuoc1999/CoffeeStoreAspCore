using CoffeeStoreAspCore.Data.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Entities
{
   public class Drink
    {
        public int Id { get; set; }
        public string   Name      { get; set; }
           public int   UnitPrice { get; set; }
           
           public int IdMenu { get; set; }
           public Status Status { set; get; }
        public Menu Menu { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
      
        public DrinkImage DrinkImage { get; set; }

    }
}
