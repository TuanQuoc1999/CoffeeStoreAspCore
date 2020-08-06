using CoffeeStoreAspCore.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog.Vouchers
{
   public class VoucherViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string CodeText { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public int TimesOfUsed { get; set; }
        public int AvailableTimes { get; set; }
        public Status status { get; set; }
    }
}
