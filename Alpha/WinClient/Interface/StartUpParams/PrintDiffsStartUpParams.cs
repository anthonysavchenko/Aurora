using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Models;
using Taumis.EnterpriseLibrary.Win.Common.Modules.StartUpParams;

namespace Taumis.Alpha.WinClient.Aurora.Interface.StartUpParams
{
    public class PrintDiffsStartUpParams : AnyStartUpParams
    {
        public PrintDiffsStartUpParams(List<Diff> diffs)
        {
            Diffs = diffs;
        }

        public List<Diff> Diffs
        {
            private set;
            get;
        }
    }
}
