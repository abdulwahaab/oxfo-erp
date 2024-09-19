using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace OxfoHome.DAL.Services
{
    public class ExcelService : IExcelService
    {
        public byte[] ExportToExcel<T>(List<T> data, string worksheetName)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add(worksheetName);

                worksheet.Cells["A1"].LoadFromCollection(data, true);

                int totalRows = data.Count + 1;

                //6 is the number of column with date
                var dateColumn = worksheet.Cells[2, 6, totalRows, 6];

                // Apply date format
                dateColumn.Style.Numberformat.Format = "dd-mm-yyyy";

                return package.GetAsByteArray();
            }
        }
    }
}