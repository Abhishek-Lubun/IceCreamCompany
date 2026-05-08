using InsecureCompany.Interfaces;
using InsecureCompany.Models;

namespace InsecureCompany.Services
{
    public class ReportService : IReportService
    {
        public decimal GetTotalSales(
            List<SaleRecord> sales)
        {
            decimal totalSales = 0;

            foreach (SaleRecord sale in sales)
            {
                totalSales =
                    totalSales + sale.TotalPrice;
            }

            return totalSales;
        }

        public List<MonthSalesReport> GetMonthWiseSales(
            List<SaleRecord> sales)
        {
            Dictionary<string, decimal> monthData =
                new Dictionary<string, decimal>();

            foreach (SaleRecord sale in sales)
            {
                string month =
                    sale.Date.ToString("yyyy-MM");

                if (!monthData.ContainsKey(month))
                {
                    monthData.Add(month, 0);
                }

                monthData[month] =
                    monthData[month] + sale.TotalPrice;
            }

            List<MonthSalesReport> result =
                new List<MonthSalesReport>();

            foreach (KeyValuePair<string, decimal> item
                     in monthData)
            {
                MonthSalesReport report =
                    new MonthSalesReport();

                report.Month = item.Key;
                report.Sales = item.Value;

                result.Add(report);
            }

            return result;
        }

        public List<PopularItemReport> GetPopularItems(
            List<SaleRecord> sales)
        {
            Dictionary<string,
                Dictionary<string, int>> data =
                new Dictionary<string,
                Dictionary<string, int>>();

            foreach (SaleRecord sale in sales)
            {
                string month =
                    sale.Date.ToString("yyyy-MM");

                if (!data.ContainsKey(month))
                {
                    data.Add(month,
                        new Dictionary<string, int>());
                }

                if (!data[month]
                    .ContainsKey(sale.SKU))
                {
                    data[month]
                        .Add(sale.SKU, 0);
                }

                data[month][sale.SKU] =
                    data[month][sale.SKU]
                    + sale.Quantity;
            }

            List<PopularItemReport> result =
                new List<PopularItemReport>();

            foreach (KeyValuePair<string,
                     Dictionary<string, int>> month
                     in data)
            {
                string itemName = "";

                int maxQuantity = 0;

                foreach (KeyValuePair<string, int> item
                         in month.Value)
                {
                    if (item.Value > maxQuantity)
                    {
                        maxQuantity = item.Value;
                        itemName = item.Key;
                    }
                }

                PopularItemReport report =
                    new PopularItemReport();

                report.Month = month.Key;
                report.Item = itemName;
                report.Quantity = maxQuantity;

                result.Add(report);
            }

            return result;
        }

        public List<RevenueReport>
            GetHighestRevenueItems(
            List<SaleRecord> sales)
        {
            Dictionary<string,
                Dictionary<string, decimal>> data =
                new Dictionary<string,
                Dictionary<string, decimal>>();

            foreach (SaleRecord sale in sales)
            {
                string month =
                    sale.Date.ToString("yyyy-MM");

                if (!data.ContainsKey(month))
                {
                    data.Add(month,
                        new Dictionary<string, decimal>());
                }

                if (!data[month]
                    .ContainsKey(sale.SKU))
                {
                    data[month]
                        .Add(sale.SKU, 0);
                }

                data[month][sale.SKU] =
                    data[month][sale.SKU]
                    + sale.TotalPrice;
            }

            List<RevenueReport> result =
                new List<RevenueReport>();

            foreach (KeyValuePair<string,
                     Dictionary<string, decimal>>
                     month in data)
            {
                string itemName = "";

                decimal maxRevenue = 0;

                foreach (KeyValuePair<string, decimal>
                         item in month.Value)
                {
                    if (item.Value > maxRevenue)
                    {
                        maxRevenue = item.Value;
                        itemName = item.Key;
                    }
                }

                RevenueReport report =
                    new RevenueReport();

                report.Month = month.Key;
                report.Item = itemName;
                report.Revenue = maxRevenue;

                result.Add(report);
            }

            return result;
        }

        public List<GrowthReport>
            GetGrowthReports(
            List<SaleRecord> sales)
        {
            Dictionary<string,
                Dictionary<string, decimal>> data =
                new Dictionary<string,
                Dictionary<string, decimal>>();

            foreach (SaleRecord sale in sales)
            {
                string item = sale.SKU;

                string month =
                    sale.Date.ToString("yyyy-MM");

                if (!data.ContainsKey(item))
                {
                    data.Add(item,
                        new Dictionary<string, decimal>());
                }

                if (!data[item]
                    .ContainsKey(month))
                {
                    data[item]
                        .Add(month, 0);
                }

                data[item][month] =
                    data[item][month]
                    + sale.TotalPrice;
            }

            List<GrowthReport> result =
                new List<GrowthReport>();

            foreach (KeyValuePair<string,
                     Dictionary<string, decimal>>
                     item in data)
            {
                decimal previous = 0;

                bool firstMonth = true;

                foreach (KeyValuePair<string, decimal>
                         month in item.Value)
                {
                    if (firstMonth)
                    {
                        previous = month.Value;
                        firstMonth = false;

                        continue;
                    }

                    decimal growth = 0;

                    if (previous != 0)
                    {
                        growth =
                            ((month.Value - previous)
                            / previous) * 100;
                    }

                    GrowthReport report =
                        new GrowthReport();

                    report.Item = item.Key;
                    report.Month = month.Key;
                    report.GrowthPercentage =
                        Math.Round(growth, 2);

                    result.Add(report);

                    previous = month.Value;
                }
            }

            return result;
        }
    }
}
