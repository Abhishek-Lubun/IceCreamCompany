using InsecureCompany.Interfaces;
using InsecureCompany.Models;

namespace InsecureCompany.Services
{
    public class DataLoaderService : IDataLoaderService
    {
        public List<SaleRecord> LoadSalesData(
           out List<ValidationError> errors)
        {
            List<SaleRecord> sales =
                new List<SaleRecord>();

            errors =
                new List<ValidationError>();

            string rawData = @"
Date,SKU,Unit Price,Quantity,Total Price
2019-01-01,Death by Chocolate,180,5,900
2019-01-01,Cake Fudge,150,1,150
2019-01-01,Cake Fudge,150,1,150
2019-01-01,Cake Fudge,150,3,450
2019-01-01,Death by Chocolate,180,1,180
2019-01-01,Vanilla Double Scoop,80,3,240
2019-01-01,Butterscotch Single Scoop,60,5,300
2019-01-01,Vanilla Single Scoop,50,5,250
2019-01-01,Cake Fudge,150,5,750
2019-01-01,Hot Chocolate Fudge,120,3,360
2019-01-01,Butterscotch Single Scoop,60,5,300
2019-01-01,Chocolate Europa Double Scoop,100,1,100
2019-01-01,Hot Chocolate Fudge,120,2,240
2019-01-01,Caramel Crunch Single Scoop,70,4,280
2019-01-01,Hot Chocolate Fudge,120,2,240
2019-01-01,Hot Chocolate Fudge,120,4,480
2019-01-01,Hot Chocolate Fudge,120,2,240
2019-01-01,Cafe Caramel,160,5,800
2019-01-01,Vanilla Double Scoop,80,4,320
2019-01-01,Butterscotch Single Scoop,60,3,180
2019-02-01,Butterscotch Single Scoop,60,3,180
2019-02-01,Vanilla Single Scoop,50,2,100
2019-02-01,Butterscotch Single Scoop,60,3,180
2019-02-01,Vanilla Double Scoop,80,1,80
2019-02-01,Death by Chocolate,180,2,360
2019-02-01,Cafe Caramel,160,2,320
2019-02-01,Pista Single Scoop,60,3,180
2019-02-01,Hot Chocolate Fudge,120,2,240
2019-02-01,Vanilla Single Scoop,50,3,150
2019-02-01,Vanilla Single Scoop,50,5,250
2019-02-01,Cake Fudge,150,1,150
2019-02-01,Vanilla Single Scoop,50,4,200
2019-02-01,Vanilla Double Scoop,80,3,240
2019-02-01,Cake Fudge,150,1,150
2019-02-01,Vanilla Double Scoop,80,5,400
2019-02-01,Hot Chocolate Fudge,120,5,600
2019-02-01,Vanilla Double Scoop,80,2,160
2019-02-01,Vanilla Double Scoop,80,3,240
2019-02-01,Hot Chocolate Fudge,120,5,600
2019-02-01,Cake Fudge,150,5,750
2019-03-01,Vanilla Single Scoop,50,5,250
2019-03-01,Cake Fudge,150,5,750
2019-03-01,Pista Single Scoop,60,1,60
2019-03-01,Butterscotch Single Scoop,60,2,120
2019-03-01,Vanilla Double Scoop,80,1,80
2019-03-01,Cafe Caramel,160,1,160
2019-03-01,Cake Fudge,150,5,750
2019-03-01,Trilogy,160,5,800
2019-03-01,Butterscotch Single Scoop,60,3,180
2019-03-01,Death by Chocolate,180,2,360
2019-03-01,Butterscotch Single Scoop,60,1,60
2019-03-01,Hot Chocolate Fudge,120,3,360
2019-03-01,Cake Fudge,150,2,300
2019-03-01,Cake Fudge,150,2,300
2019-03-01,Vanilla Single Scoop,50,4,100
2019-03-01,Cafe Caramel,160,0,160
2019-03-01,Cake Fudge,150,5,750
2019-03-01,Cafe Caramel,160,5,800
2019-03-01,Almond Fudge,150,1,150
2019-03-01,Cake Fudge,150,1,150

";

            string[] lines = rawData.Split('\n');

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(',');

                DateTime date;
                decimal unitPrice;
                int quantity;
                decimal totalPrice;

                bool validDate =
                    DateTime.TryParse(parts[0], out date);

                bool validUnitPrice =
                    decimal.TryParse(parts[2], out unitPrice);

                bool validQuantity =
                    int.TryParse(parts[3], out quantity);

                bool validTotal =
                    decimal.TryParse(parts[4], out totalPrice);

                if (!validDate)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 1,
                        Message = "Date is malformed"
                    });

                    continue;
                }

                if (!validUnitPrice || unitPrice < 0)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 1,
                        Message = "Unit Price is less than 0"
                    });

                    continue;
                }

                if (!validQuantity || quantity < 1)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 1,
                        Message = "Quantity is less than 1"
                    });

                    continue;
                }

                if (!validTotal || totalPrice < 0)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 1,
                        Message = "Total Price is less than 0"
                    });

                    continue;
                }

                if ((unitPrice * quantity) != totalPrice)
                {
                    errors.Add(new ValidationError
                    {
                        LineNumber = i + 1,
                        Message = "UnitPrice * Quantity is not equal to TotalPrice"
                    });

                    continue;
                }

                SaleRecord sale =
                    new SaleRecord();

                sale.Date = date;
                sale.SKU = parts[1];
                sale.UnitPrice = unitPrice;
                sale.Quantity = quantity;
                sale.TotalPrice = totalPrice;

                sales.Add(sale);
            }

            return sales;
        }
    }
}
