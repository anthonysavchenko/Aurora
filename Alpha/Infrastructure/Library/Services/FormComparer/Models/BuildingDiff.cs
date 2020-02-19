using Taumis.Alpha.Infrastructure.Library.Services.FormParser.Models;

namespace Taumis.Alpha.Infrastructure.Library.Services.FormComparer.Models
{
    public class BuildingDiff : Diff
    {
        public Building PrintFormBuilding;
        public Building FillFormBuilding;
        public BuildingDiffType BuildingDiffType;
    }
}
