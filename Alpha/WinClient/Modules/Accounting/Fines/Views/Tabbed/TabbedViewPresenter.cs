using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseTabbedView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Fines.View.Tabbed
{
    public class TabbedViewPresenter : BaseTabbedViewPresenter<IBaseTabbedView, FineDoc>
    {
        /// <summary>
        /// Создает новый объект домена
        /// </summary>
        /// <returns>Новый объект домена</returns>
        protected override FineDoc CreateNewItem()
        {
            return new FineDoc()
            {
                ID = Guid.NewGuid().ToString(),
                Period = ServerTime.GetPeriodInfo().FirstUncharged
            };
        }
    }
}