using InsecureCompany.Models;

namespace InsecureCompany.Interfaces
{
    public interface IReportService
    {
        decimal GetTotalSales(List<SaleRecord> sales);

        List<MonthSalesReport> GetMonthWiseSales(
            List<SaleRecord> sales);

        List<PopularItemReport> GetPopularItems(
            List<SaleRecord> sales);

        List<RevenueReport> GetHighestRevenueItems(
            List<SaleRecord> sales);

        List<GrowthReport> GetGrowthReports(
            List<SaleRecord> sales);
    }
}
