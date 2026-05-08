using InsecureCompany.Models;

namespace InsecureCompany.Interfaces
{
    public interface IDataLoaderService
    {
        List<SaleRecord> LoadSalesData(
            out List<ValidationError> errors);
    }
}
