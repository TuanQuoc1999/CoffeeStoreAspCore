using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog.BillDeatils
{
   public class BillDetailUpdateRequest
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdDrink { get; set; }
        public int Quantity { get; set; }
    }
}
