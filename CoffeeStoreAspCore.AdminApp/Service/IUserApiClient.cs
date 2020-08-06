using CoffeeStoreAspCore.ViewModels.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.AdminApp.Service
{
   public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
    }
}
