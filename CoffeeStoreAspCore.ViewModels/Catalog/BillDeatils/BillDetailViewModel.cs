using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog.BillOrders
{
   public class BillDetailViewModel
    {
      
        public int IdDrink { get; set; }
        public int Quantity { get; set; }
        public string NameDrink { get; set; }
        public decimal Unitprice { get; set; }
    }
}
