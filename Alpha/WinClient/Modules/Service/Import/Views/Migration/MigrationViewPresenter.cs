using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.MigrationWorkers;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;
using Taumis.EnterpriseLibrary.Win.Services;
using Taumis.Infrastructure.Interface.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Migration
{
    /// <summary>
    /// Презентер вью формы
    /// </summary>
    public class MigrationViewPresenter : BasePresenter<IMigrationView>
    {
        /// <summary>
        /// Выполняет миграцию
        /// </summary>
        public void Migrate()
        {
        }
    }
}