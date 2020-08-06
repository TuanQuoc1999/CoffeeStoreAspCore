
using CoffeeStoreAspCore.Data.EF;
using CoffeeStoreAspCore.Data.Entities;
using CoffeeStoreAspCore.Data.Enums;
using CoffeeStoreAspCore.Utilities;
using CoffeeStoreAspCore.ViewModels.Catalog;
using CoffeeStoreAspCore.ViewModels.Catalog.BillDeatils;
using CoffeeStoreAspCore.ViewModels.Catalog.BillOrders;
using CoffeeStoreAspCore.ViewModels.Catalog.Bills;
using CoffeeStoreAspCore.ViewModels.Catalog.Report;
using CoffeeStoreAspCore.ViewModels.Catalog.Vouchers;
using Dapper;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Application.Catalog.Bills
{
   public class ManageBillService: IManageBillService
    {
        private readonly StoreDBContext _context;
        public ManageBillService(StoreDBContext context)
        {
            _context = context;
           
        }

        public async Task<int> CreateBill(BillCreateRequest billrequest)
        {
            var details = new List<OrderDetail>();
            foreach (var item in billrequest.BillDetails)
            {
                details.Add(new OrderDetail()
                {
                   IdDrink=item.IdDrink,
                   Quantity=item.Quantity
                });
            }

            var bill = new Order()
            {
                IdUser = billrequest.IdUser,
                ActualTotalAmount = billrequest.ActualTotalAmount,
                CalculatedTotalAmount = billrequest.CalculatedTotalAmount,
                Days = billrequest.Days,
                IdVoucher = billrequest.IdVoucher,
                TotalQuantity = billrequest.TotalQuantity,
               OrderDetails = details


            };
       
            _context.Order.Add(bill);

        /*    foreach (var detail in details)
            {

                detail.IdOrder = bill.Id;
                _context.OrderDetails.Add(detail);
            }*/
            await _context.SaveChangesAsync();
            return bill.Id;
        }

        public async Task<int> CreateBillDetail(BillDetailCreateRequest request)
        {
            var billDetail = new OrderDetail()
            {
                IdDrink = request.IdDrink,
             
                Quantity = request.Quantity
            };
            _context.OrderDetails.Add(billDetail);
            await _context.SaveChangesAsync();
            return billDetail.Id;
        }

        public async Task<List<BillViewModel>> GetAll()
        {
         
            var bills = await _context.Order.Include(x => x.User).Include(x=>x.Voucher).Include(x => x.OrderDetails).ThenInclude(x=>x.Drink).ToListAsync();
            var data = bills.Select(x => new BillViewModel()
            {
                Id = x.Id,
                IdUser = x.IdUser,
                UserName = x.User.UserName,
                TotalQuantity = x.TotalQuantity,
                CalculatedTotalAmount = x.CalculatedTotalAmount,
                ActualTotalAmount = x.ActualTotalAmount,
                Days = x.Days,
                IdVoucher = x.IdVoucher,
                DiscountAmount =  x.Voucher != null ? x.Voucher.DiscountAmount:null,
                DiscountPercent = x.Voucher != null ? x.Voucher.DiscountPercent:null,
                  Address=x.User.Address,
                CodeText = x.Voucher != null ? x.Voucher.CodeText : null,
                status = x.Status,
                BillDetails = x.OrderDetails.Select(x => new BillDetailViewModel()
                {

                    IdDrink = x.IdDrink,
                    Quantity = x.Quantity,
                   NameDrink=x.Drink.Name,
                   Unitprice=x.Drink.UnitPrice

                   

                }).ToList()

            }).ToList();
            return data;
        }

        public async Task<List<RevenueReportViewModel>> Report(DateTime fromdate , DateTime todate)
        {
           var query = from o in _context.Order
                       where o.Days >= fromdate && o.Days <= todate
                       group o by o.Days into OrderGroup
                     
                       select new
                       {
                           Days = OrderGroup.Key,
                           SumTotal = OrderGroup.Sum(x => x.ActualTotalAmount),
                       };

            var data = await query.Select(x => new RevenueReportViewModel()
            {
                Date = x.Days,
                Revenue=x.SumTotal
            }).ToListAsync();
            return data;
        }

      

        public async Task<List<BillDetailViewModel>> GetAllBillDetail()
        {
            var query = from d in _context.OrderDetails
                       
                        select new { d };
            var data = await query.Select(x => new BillDetailViewModel()
            {
                
                IdDrink = x.d.IdDrink,
                Quantity = x.d.Quantity
               

            }).ToListAsync();
            return data;
        }

      

        public async Task<BillViewModel> GetById(int idBill)
        {
            var x = await _context.Order.Where(x => x.Id == idBill).Include(x => x.User).Include(x => x.Voucher).Include(x => x.OrderDetails).ThenInclude(x => x.Drink).FirstOrDefaultAsync();
            var billViewModel = new BillViewModel()
            {
                Id = x.Id,
                IdUser = x.IdUser,
                UserName = x.User.UserName,
                Address = x.User.Address,
                TotalQuantity = x.TotalQuantity,
                CalculatedTotalAmount = x.CalculatedTotalAmount,
                ActualTotalAmount = x.ActualTotalAmount,
                Days = x.Days,
                IdVoucher = x.IdVoucher,
                CodeText = x.Voucher != null ? x.Voucher.CodeText : null,
                status = x.Status,
                DiscountAmount = x.Voucher != null ? x.Voucher.DiscountAmount : null,
                 DiscountPercent= x.Voucher != null ? x.Voucher.DiscountPercent : null,

                BillDetails = x.OrderDetails.Select(x => new BillDetailViewModel()
                {

                    IdDrink = x.IdDrink,
                    Quantity = x.Quantity,
                    NameDrink = x.Drink.Name,
                     Unitprice = x.Drink.UnitPrice


                }).ToList()

            };
            return billViewModel;
        }

        public async Task<bool> UpdateStatus(int orderId, OrderStatus status)
        {

            var Bill = await _context.Order.FindAsync(orderId);
            if (Bill == null) throw new StoreException($"Cannot find ");
            Bill.Status = status ;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<DrinkReportViewModel>> ReportProduct(DateTime fromdate, DateTime todate)
        {
            var query = from o in _context.Order
                        join od in _context.OrderDetails on o.Id equals od.IdOrder
                        join d in _context.Drinks on od.IdDrink equals d.Id
                        where o.Days >= fromdate && o.Days <= todate
                        group od by od.Drink.Name into OrderGroup

                        select new
                        {
                            Drink = OrderGroup.Key,
                            SumTotal = OrderGroup.Sum(x => x.Quantity )
                           
                        };

            var data = await query.Select(x => new DrinkReportViewModel()
            {
                NameDrink = x.Drink,
                Revenue = x.SumTotal
            }).ToListAsync();
            return data;
        }
    }
}
