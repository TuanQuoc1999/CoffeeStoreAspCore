using CoffeeStoreAspCore.ViewModels.Catalog.BillDeatils;
using CoffeeStoreAspCore.ViewModels.Catalog.BillOrders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog.Bills
{
  public  class BillCreateRequest
    {
       
        public DateTime Days { get; set; }
        public Guid IdUser { set; get; }
        public decimal CalculatedTotalAmount { set; get; }
        public decimal ActualTotalAmount { set; get; }
        public int TotalQuantity { set; get; }
        public int? IdVoucher { set; get; }
        public List<BillDetailCreateRequest> BillDetails { set; get; }
    }
}
