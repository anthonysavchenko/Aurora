using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsUploader.DecFormsParser.RouteFormParser
{
    static public class FileParser
    {
        static public bool IsRouteForm(
            ExcelSheet source,
            out string message)
        {
            message = null;

            if (source.RowsCount < RowParser.FIRST_ROW)
            {
                message = "Файл не соответствует формату \"Маршрутный лист\", так как обнаружена следующая ошибка. " +
                    "В файле заполнено слишком мало строк. Форматом предусмотрено наличие данных о первой " +
                    $"квартире в строке {RowParser.FIRST_ROW}, в данном случае строк в файле заполнено меньше.";
                return false;
            }

            if (!RowParser.ParseRow(
                source,
                RowParser.FIRST_ROW,
                out _,
                out _,
                out _,
                out string rowMessage))
            {
                message = "Файл не соответствует формату \"Маршрутный лист\", так как обнаружена следующая ошибка. "
                    + rowMessage;
                return false;
            }

            return true;
        }

        static public void ParseFile(
            ExcelSheet source,
            DecFormsUploadPoses uploadPos,
            out string message)
        {
            if (!RowParser.ParseRows(source,
                out List<RouteFormPoses> poses,
                out string street,
                out string building,
                out message))
            {
                DecFormsUploadPosHandler.UpdateUploadPosWithError(
                    uploadPos,
                    $"Ошибка при распознавании файла. {message}");
            }

            RouteFormHandler.CreateForm(uploadPos, street, building, poses);
        }
    }
}
