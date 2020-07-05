using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsParser.RouteFormParser
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
                DecFormsParser.FileParser.SaveError(uploadPos, $"Ошибка при распознавании файла. {message}");
            }

            RouteForms form =
                new RouteForms()
                {
                    Street = street,
                    Building = building,
                };

            using (Entities db = new Entities())
            {
                db.DecFormsUploadPoses.Attach(uploadPos);
                db.RouteForms.AddObject(form);

                uploadPos.FormType = (byte)DecFormsType.RouteForm;
                uploadPos.RouteForm = form;

                form.DecFormsUploadPoses = uploadPos;

                foreach (RouteFormPoses pos in poses)
                {
                    pos.RouteForms = form;
                    db.RouteFormPoses.AddObject(pos);
                }

                db.SaveChanges();
            }
        }
    }
}
