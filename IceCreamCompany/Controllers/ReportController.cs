using InsecureCompany.Interfaces;
using InsecureCompany.Models;
using Microsoft.AspNetCore.Mvc;

namespace IceCreamCompany.Controllers
{
    public class ReportController : Controller
    {
        private readonly IDataLoaderService _dataLoaderService;

        private readonly IReportService _reportService;
        public ReportController(IDataLoaderService dataLoaderService,
           IReportService reportService)
        {
            _dataLoaderService = dataLoaderService;
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            try
            {
                List<ValidationError> errors;

                List<SaleRecord> sales =
                    _dataLoaderService.LoadSalesData(out errors);

                ReportViewModel model =
                    new ReportViewModel();

                model.TotalSales =
                    _reportService.GetTotalSales(sales);

                model.MonthSales =
                    _reportService.GetMonthWiseSales(sales);

                model.PopularItems =
                    _reportService.GetPopularItems(sales);

                model.RevenueReports =
                    _reportService.GetHighestRevenueItems(sales);

                model.GrowthReports =
                    _reportService.GetGrowthReports(sales);

                model.ValidationErrors = errors;

                return View(model);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}

/*
-----------------------------------------------------------
Assessment Questions
-----------------------------------------------------------

1) What was the most complex part of the assignment for you personally and why?

Answer:
The most complex part for me was generating the reports without using LINQ or a database.
I had to manually manage data using Lists and Dictionaries and write custom logic for grouping,
calculating totals, and generating month-wise reports. It required careful handling of loops
and conditions to avoid mistakes.

-----------------------------------------------------------

2) Describe a bug you expect to hit while implementing this and how you would debug it.

Answer:
One possible bug is incorrect report calculations because of invalid or malformed data.
For example, if the date format is wrong or TotalPrice does not match UnitPrice * Quantity,
the reports may show incorrect results.

To debug this issue, I would:
- Check the validation error table
- Use breakpoints while looping through records
- Verify values step by step in Visual Studio debugger
- Print intermediate values to confirm calculations

-----------------------------------------------------------

3) Does your solution handle larger data sets without any performance implications?

Answer:
Yes, the solution can handle moderately large data sets because it uses efficient data
structures like Lists and Dictionaries. The logic processes data using loops without
extra database calls.

However, for extremely large files, performance may reduce because all data is stored
in memory. In real-world applications, pagination, caching, or database optimization
could be added for better scalability.

-----------------------------------------------------------
*/
