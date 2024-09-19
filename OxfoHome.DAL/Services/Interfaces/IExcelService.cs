
namespace OxfoHome.DAL.Services
{
    public interface IExcelService
    {
        byte[] ExportToExcel<T>(List<T> data, string worksheetName);
    }
}