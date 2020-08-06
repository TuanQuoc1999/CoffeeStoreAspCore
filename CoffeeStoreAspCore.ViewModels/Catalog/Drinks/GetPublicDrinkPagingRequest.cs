using CoffeeStoreAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.DrinkRepo
{
  public  class GetPublicDrinkPagingRequest : PagingRequestBase
    {
        public int? IdMenu { get; set; }
    }
}
