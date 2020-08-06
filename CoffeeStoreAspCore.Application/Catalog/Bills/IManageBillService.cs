
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.ViewModels.Catalog;
using CoffeeStoreAspCore.ViewModels.Catalog.BillDeatils;
using CoffeeStoreAspCore.ViewModels.Catalog.BillOrders;
using CoffeeStoreAspCore.ViewModels.Catalog.Bills;
using CoffeeStoreAspCore.ViewModels.Catalog.Report;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Application.Catalog.Bills
{
  public  interface IManageBillService
    {
        Task<int> CreateBill(BillCreateRequest billrequest);
        Task<int> CreateBillDetail(BillDetailCreateRequest request);
        Task<BillViewModel> GetById(int idBill);
        Task<List<BillViewModel>> GetAll();
        
        Task<List<BillDetailViewModel>> GetAllBillDetail();
        Task<List<RevenueReportViewModel>> Report(DateTime fromdate,DateTime todate);
        Task<List<DrinkReportViewModel>> ReportProduct(DateTime fromdate, DateTime todate);
        Task<bool> UpdateStatus(int orderId, OrderStatus status);
        
    }
}
