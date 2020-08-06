using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog.Vouchers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Application.Catalog.Vouchers
{
   public interface IManageVoucherService
    {
        Task<int> Create(VoucherCreateRequest request);

        Task<int> Update(VoucherUpdateRequest request);

        Task<VoucherViewModel> GetByCode(string code);

        Task<VoucherViewModel> GetById(int id);
        Task<List<VoucherViewModel>> GetAll();

        Task<bool> ChangeStatus(int idVoucher, Status status);
    }
}
