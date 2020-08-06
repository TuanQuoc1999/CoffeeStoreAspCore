using CoffeeStoreAspCore.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Application.Catalog.Contacts
{
  public  interface IManagerContacService
    {
        Task<int> Create(ContactCreateRequest request);

        Task<List<ContactViewModel>> GetAll();

        Task<ContactViewModel> GetById(int id);
      
    }
}
