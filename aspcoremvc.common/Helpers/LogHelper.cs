using System.IO;
using System.Text;
using Serilog;

namespace Common.Helpers
{
    public static class LogHelper
    {
        // Bu method mevcut custom nesne kopyasını oluşturarak aynı türden yeni bir object yaratır.
        public static void Log(this ILogger logger, string str)
        {
            //var logger = HttpHelper.GetService<ILogger>();
            logger.Information(str);
        }

        public static void Log(string str)
        {
            var logger = HttpHelper.GetService<ILogger>();
            logger.Information(str);
        }

        public static void LogCustom(string str, bool recreate, string fullFileName)
        {
            //var enviroment = HttpHelper.GetService<IHostingEnvironment>();
            //string myDocPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), $"{fullFileName}");

            if (recreate && File.Exists(filepath))
                File.Delete(filepath);

            File.WriteAllText(filepath, str, Encoding.UTF8);
        }
    }
}