using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Library.Services.Handlers;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.PrivateValuesUploader.PrivateValuesParser.PrivateValuesFormParser
{
    static public class FileParser
    {
        static public bool IsPrivateValuesForm(
            ExcelSheet source,
            out string message)
        {
            message = null;

            if (source.RowsCount < RowParser.FIRST_ROW)
            {
                message = "Файл не соответствует формату \"Форма для заполнения (с текущими показаниями)\", " +
                    "обнаружена следующая ошибка. В файле заполнено слишком мало строк. Форматом предусмотрено " +
                    $"наличие данных о первой квартире в строке {RowParser.FIRST_ROW}, в данном случае строк " +
                    $"в файле заполнено меньше.";
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
                message = "Файл не соответствует формату \"Форма для заполнения (с текущими показаниями)\", " +
                    "обнаружена следующая ошибка. "
                    + rowMessage;
                return false;
            }

            return true;
        }

        static public void ParseFile(
            ExcelSheet source,
            PrivateValuesForms form,
            out string message)
        {
            if (!RowParser.ParseRows(source,
                out List<PrivateValuesFormPoses> poses,
                out string street,
                out string building,
                out message))
            {
                PrivateValuesFormHandler.UpdateFormWithError(
                    form,
                    $"Ошибка при распознавании файла. {message}");
            }

            PrivateValuesFormHandler.UpdateForm(form, street, building, poses);
        }
    }
}
