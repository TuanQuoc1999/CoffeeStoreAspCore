using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog.Vouchers
{
   public class VoucherCreateRequest
    {
    
        public string Name { get; set; }
        public string CodeText { get; set; }
        public int? DiscountPercent { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public int TimesOfUsed { get; set; }

    }
}
