using System.Collections.Generic;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Enums;
using static Taumis.Alpha.Infrastructure.Library.Services.Excel.Excel2007Worker;

namespace Taumis.Alpha.Infrastructure.Library.Services.DecFormsParser.FillFormParser
{
    static public class FileParser
    {
        static public bool IsFillForm(
            ExcelSheet source,
            out string message)
        {
            message = null;

            if (source.RowsCount < RowParser.FIRST_ROW)
            {
                message = "Файл не соответствует формату \"Форма для заполнения\", обнаружена следующая ошибка. " +
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
                message = "Файл не соответствует формату \"Форма для заполнения\", обнаружена следующая ошибка. "
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
                out List<FillFormPoses> poses,
                out string street,
                out string building,
                out message))
            {
                DecFormsParser.FileParser.SaveError(uploadPos, $"Ошибка при распознавании файла. {message}");
            }

            FillForms form =
                new FillForms()
                {
                    Street = street,
                    Building = building,
                };

            using (Entities db = new Entities())
            {
                db.DecFormsUploadPoses.Attach(uploadPos);
                db.FillForms.AddObject(form);

                uploadPos.FormType = (byte)DecFormsType.FillForm;
                uploadPos.FillForm = form;

                form.DecFormsUploadPoses = uploadPos;

                foreach (FillFormPoses pos in poses)
                {
                    pos.FillForms = form;
                    db.FillFormPoses.AddObject(pos);
                }

                db.SaveChanges();
            }
        }
    }
}
