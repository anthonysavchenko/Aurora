using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Enums;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Layout
{
    public static class DataValidator
    {
        public static bool ValidateFilePath(string filePath, out string errorMessage)
        {
            bool _result = !string.IsNullOrEmpty(filePath);
            errorMessage = _result ? string.Empty : "Выберите файл для импорта данных";

            return _result;
        }

        public static bool ValidateImportPeriod(DateTime period, WizardAction wizardAction)
        {
            bool _result;

            using (Entities _db = new Entities())
            {
                _result = wizardAction == WizardAction.ImportPublicPlaceServiceVolumes
                    ? _db.PublicPlaceServiceVolumes.Any(x => x.Period == period)
                    : wizardAction == WizardAction.ImportElectricitySharedCounterVolumes
                        ? _db.ElectricitySharedCounterVolumes.Any(x => x.Period == period)
                        : _db.PrivateCounterValues.Any(x => x.Period == period);
            }

            if (_result)
            {
                DialogResult _answer = MessageBox.Show(
                        $"Данные за {period:MMMM yyyy} уже импортированы, при новом импорте они будут перезаписаны. Продолжить?",
                        $"Импорт данных за {period:MMMM yyyy}",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                _result = _answer == DialogResult.Yes;
            }
            else
            {
                _result = true;
            }

            return _result;
        }

        public static bool ValidateAddress(string buildingId, out string errorMessage)
        {
            bool _result = !string.IsNullOrEmpty(buildingId);
            errorMessage = _result ? string.Empty : "Не указан дом";
            return _result;
        }
    }
}
