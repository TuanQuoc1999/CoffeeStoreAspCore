using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.Catalog.Users
{
  public  class RoleViewModel
    {
        public Guid? Id { set; get; }

        public string Name { set; get; }

        public string Description { set; get; }
    }
}
