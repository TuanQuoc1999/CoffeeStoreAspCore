using System;
using System.Collections.Generic;
using System.Text;
using CoffeeStoreAspCore.Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace CoffeeStoreAspCore.Data.Entities
{
   public class User: IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }
    

        public List<Order> Orders { get; set; }
    }
}
