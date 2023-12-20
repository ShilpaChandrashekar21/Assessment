using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magneto.utilites
{
    internal class ShippingExcelUtil
    {
        public static List<ShippingData> ReadShippingExcelData(string excelFilePath, string sheetName)
        {
            List<ShippingData> excelShippingDataList = new List<ShippingData>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });

                    var dataTable = result.Tables[sheetName];

                    if (dataTable != null)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            ShippingData shippingExcelData = new ShippingData()
                            {
                                Email = GetValueOrDefault(row, "email"),
                                FirstName = GetValueOrDefault(row, "firstName"),
                                LastName = GetValueOrDefault(row, "lastName"),
                                Company = GetValueOrDefault(row, "company"),
                                Address = GetValueOrDefault(row, "address"),
                                City = GetValueOrDefault(row, "city"),
                                State = GetValueOrDefault(row, "state"),
                                PostalCode = GetValueOrDefault(row, "zip"),
                                PhoneNumber = GetValueOrDefault(row, "phNumber")


                            };
                            excelShippingDataList.Add(shippingExcelData);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sheet '{sheetName}' not found in the Excel file.");
                    }
                }
            }

            return excelShippingDataList;
        }

        static string? GetValueOrDefault(DataRow row, string columnName)
        {
            Console.WriteLine(row + "  " + columnName);
            return row.Table.Columns.Contains(columnName) ? row[columnName]?.ToString() : null;
        }
    }
}
