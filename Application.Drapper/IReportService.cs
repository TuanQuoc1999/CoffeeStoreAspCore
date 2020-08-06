using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeStoreAspCore.Application.Drapper
{
    public interface IReportService
    {
        Task<IEnumerable<RevenueReportViewModel>> GetReportAsync(string fromDate, string toDate);
    }
}
