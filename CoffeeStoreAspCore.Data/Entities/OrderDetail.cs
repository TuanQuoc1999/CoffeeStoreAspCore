
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdDrink { get; set; }
        public int Quantity { get; set; }
      
        public Order Order { get; set; }
       
        public Drink Drink { get; set; }


    }
}
