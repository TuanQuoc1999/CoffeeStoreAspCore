using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.ViewModels.Catalog.Contacts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CoffeeStoreAspCore.Application.Catalog.Contacts
{
    public class ManageContactService : IManagerContacService
    {
        private readonly StoreDBContext _context;

        public ManageContactService(StoreDBContext context)
        {
            _context = context;

        }
        public async Task<int> Create(ContactCreateRequest request)
        {
            var contact = new Contact()
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Message = request.Message
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return contact.Id;

        }

        public async Task<List<ContactViewModel>> GetAll()
        {
            var query = from c in _context.Contacts
                      
                        select new { c };
            var data =await query.Select(x => new ContactViewModel()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                Email = x.c.Email,
                PhoneNumber = x.c.PhoneNumber,
                Message = x.c.Message
            }).ToListAsync();
            return data;

        }

        public async Task<ContactViewModel> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            var contactVm = new ContactViewModel() { 
              Id= contact.Id,
              Name=contact.Name,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber,
                Message = contact.Message
            };
            return contactVm;
        }
    }
}
