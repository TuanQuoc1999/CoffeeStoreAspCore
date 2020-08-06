using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeStoreAspCore.ViewModels.System
{
   public class RegisterRequest
    {
        public RegisterRequest()
        {
            Roles = new List<string>();
        }
        public Guid? Id { set; get; }
        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<string> Roles { get; set; }
    }
}
