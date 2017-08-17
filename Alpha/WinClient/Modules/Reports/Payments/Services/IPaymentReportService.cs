using System;
using System.Collections.Generic;
using System.Data;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Views.List;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.Payments.Services
{
    public interface IPaymentReportService
    {
        List<Column> GetColumns();
        DataTable GetData(DateTime since, DateTime till);
    }
}
