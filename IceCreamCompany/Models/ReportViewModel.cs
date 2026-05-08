namespace InsecureCompany.Models
{
    public class ReportViewModel
    {
        public decimal TotalSales { get; set; }

        public List<MonthSalesReport> MonthSales { get; set; }

        public List<PopularItemReport> PopularItems { get; set; }

        public List<RevenueReport> RevenueReports { get; set; }

        public List<GrowthReport> GrowthReports { get; set; }

        public List<ValidationError> ValidationErrors { get; set; }
    }
}
