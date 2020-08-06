using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog.Vouchers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CoffeeStoreAspCore.Utilities;

namespace CoffeeStoreAspCore.Application.Catalog.Vouchers
{
  public  class ManageVoucherService : IManageVoucherService
    {
        private readonly StoreDBContext _context;

        public ManageVoucherService(StoreDBContext context)
        {
            _context = context;

        }
        public async Task<bool> ChangeStatus(int idVoucher, Status status)
        {
            var voucher = await _context.Vouchers.FindAsync(idVoucher);
            if (voucher == null) throw new StoreException($"Cannot find ");
            voucher.Status = status;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> Create(VoucherCreateRequest request)
        {
            var voucher = new Voucher()
            {
                Name = request.Name,
                CodeText = request.CodeText,
                DiscountAmount = request.DiscountAmount,

                DiscountPercent = request.DiscountPercent,
                StartDay= request.StartDay,
                EndDay=request.EndDay,
                TimesOfUsed =request.TimesOfUsed
                
               
            };
            _context.Vouchers.Add(voucher);
            await _context.SaveChangesAsync();
            return voucher.Id;
        }

        public async Task<List<VoucherViewModel>> GetAll()
        {
          var  Vouchers = await _context.Vouchers.Where(x => x.Status == Status.Active)
                .Select(x=>new VoucherViewModel()
                {
                    Id=x.Id,
                    Name = x.Name,
                    CodeText = x.CodeText,
                    DiscountAmount = x.DiscountAmount,

                    DiscountPercent = x.DiscountPercent,
                    StartDay = x.StartDay,
                    EndDay = x.EndDay,
                    TimesOfUsed = x.TimesOfUsed,
                    AvailableTimes=x.AvailableTimes,
                     status =x.Status
                }) .ToListAsync();
            return Vouchers;
        }

        public async  Task<VoucherViewModel> GetByCode(string code)
        {
            var x = await _context.Vouchers.Where(x => x.CodeText == code).FirstOrDefaultAsync();
            var VoucherViewModel = new VoucherViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                CodeText = x.CodeText,
                DiscountAmount = x.DiscountAmount,

                DiscountPercent = x.DiscountPercent,
                StartDay = x.StartDay,
                EndDay = x.EndDay,
                TimesOfUsed = x.TimesOfUsed,
                AvailableTimes = x.AvailableTimes,
                status=x.Status
                
            };
            return VoucherViewModel;

        }

        public async Task<VoucherViewModel> GetById(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            var voucherVm = new VoucherViewModel()
            {
                Id = voucher.Id,
                Name = voucher.Name,
                CodeText = voucher.CodeText,
                DiscountAmount = voucher.DiscountAmount,

                DiscountPercent = voucher.DiscountPercent,
                StartDay = voucher.StartDay,
                EndDay = voucher.EndDay,
                TimesOfUsed = voucher.TimesOfUsed,
                AvailableTimes = voucher.AvailableTimes,
                status=voucher.Status
                 
            };
            return voucherVm;

        }

        public async Task<int> Update(VoucherUpdateRequest request)
        {
            var voucher = await _context.Vouchers.FindAsync(request.Id);
            if (voucher == null) throw new StoreException($"Cannot find ");

            voucher.Id = request.Id;
            voucher.Name = request.Name;
            voucher.CodeText = request.CodeText;
            voucher.DiscountAmount = request.DiscountAmount;

            voucher.DiscountPercent = request.DiscountPercent;
            voucher.StartDay = request.StartDay;
            voucher.EndDay = request.EndDay;
            voucher.TimesOfUsed = request.TimesOfUsed;
              

            return await _context.SaveChangesAsync();
        }
    }
}
