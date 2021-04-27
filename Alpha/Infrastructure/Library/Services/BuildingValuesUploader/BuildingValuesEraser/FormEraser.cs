using Taumis.Alpha.Infrastructure.Library.Services.Handlers;

namespace Taumis.Alpha.Infrastructure.Library.Services.BuildingValuesUploader.BuildingValuesEraser
{
    public static class FormEraser
    {
        public static void EraseForm(int formID)
        {
            BuildingCounterValueHandler.Delete(formID);
            BuildingCounterHandler.DeleteWithNoValues();
        }
    }
}
