using CoffeeStoreAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.DrinkRepo
{
    class GetManageDrinkPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }

        public List<int> IdMenus { get; set; }
    }
}
