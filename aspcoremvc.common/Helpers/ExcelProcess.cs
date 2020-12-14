using System.IO;
using System.Data;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;

namespace Common.Helpers
{
    public static class ExcelProcess
    {
        public static async Task<DataTable> ReadExcelFromHttp(IFormFile file, bool firstRowHeader = true)
        {
            var filePath = Path.GetTempFileName();

            if (file.Length > 0)
                using (var stream = new FileStream(filePath, FileMode.Create))
                    await file.CopyToAsync(stream);

            return ReadExcelFile(filePath, firstRowHeader);
        }

        public static DataTable ReadExcelFile(string path, bool firstRowHeader = true)
        {
            var result = new DataSet();

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    do
                    {
                        while (reader.Read())
                        {
                        }

                    } while (reader.NextResult());

                    result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = firstRowHeader
                        }
                    });
                }
            }

            return result.Tables[0];
        }
    }
}