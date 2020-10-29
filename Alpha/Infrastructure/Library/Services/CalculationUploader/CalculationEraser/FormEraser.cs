using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.CalculationUploader.CalculationEraser
{
    public static class FormEraser
    {
        public static void EraseForm(int formID)
        {
            LegalEntityCalculationValuesHandler.Delete(formID);
            LegalEntityHandler.DeleteWithNoValues();

            CustomerCalculationValuesHandler.Delete(formID);
            CustomerHandler.DeleteWithNoValues();

            BuildingCounterCalculationValuesHandler.Delete(formID);
            BuildingCounterHandler.DeleteWithNoValues();

            BuildingCalculationValueHandler.Delete(formID);
        }
    }
}
