using System;
using System.IO;
using Taumis.Alpha.Infrastructure.Library.Services.Excel;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationParser
{
    public static class FileParser
    {
        public static void ParseFile(int uploadID, Excel2007Worker worker, string file)
        {
            var fileID = CalculationFileHandler.CreateFile(Path.GetFileName(file), uploadID);

            try
            {
                worker.OpenFile(file);
                Excel2007Worker.ExcelSheet sheet = worker.GetSheet(1);
                var rowsCount = sheet.RowsCount;

                if (rowsCount > 0)
                {
                    FormParser.ParseForm(
                        fileID,
                        sheet,
                        rowsCount);
                }
            }
            catch (Exception exception)
            {
                CalculationFileHandler.UpdateParsingError(
                    fileID,
                    "Ошибка при работе с файлом Excel. Убедитесь, что он не открыт в другой программе.",
                    exception);
            }
            finally
            {
                worker.Close();
            }
        }
    }
}
