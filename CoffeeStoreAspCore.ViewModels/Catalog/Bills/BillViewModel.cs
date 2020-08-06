using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog.BillOrders;
using CoffeeStoreAspCore.ViewModels.Catalog.Vouchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog.Bills
{
 public   class BillViewModel
    {
        public int Id { get; set; }
        public DateTime Days { get; set; }
        public Guid IdUser { set; get; }

        public string UserName { set; get; }
        public string Address { set; get; }
        public decimal CalculatedTotalAmount { set; get; }
        public decimal ActualTotalAmount { set; get; }
        public int TotalQuantity { set; get; }
        public int? IdVoucher { set; get; }
        
        public string CodeText { set; get; }
        public int? DiscountPercent { set; get; }
        public decimal? DiscountAmount { set; get; }
        public List<BillDetailViewModel> BillDetails { set; get; }

        public OrderStatus status { set; get; }

    }
}
