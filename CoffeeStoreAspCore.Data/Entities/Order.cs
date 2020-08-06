using CoffeeStoreAspCore.Data.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.Data.Entities
{
   public class Order 
    {
        public int Id { get; set; }
        public DateTime Days{ get; set; }
        public Guid IdUser { set; get; }
        public decimal CalculatedTotalAmount { set; get; }
        public decimal ActualTotalAmount { set; get; }
        public int TotalQuantity { set; get; }
        public int? IdVoucher  { set; get; }
        public  OrderStatus Status { set; get; }
        public List<OrderDetail> OrderDetails { get; set; }

        public Voucher Voucher { get; set; }
        public User User { get; set; }
    }
}
