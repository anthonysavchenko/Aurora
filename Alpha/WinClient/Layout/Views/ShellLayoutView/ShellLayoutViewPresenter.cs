using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using System;
using Taumis.EnterpriseLibrary.Win.Constants;
using Taumis.Infrastructure.Interface;

namespace Taumis.Infrastructure.Layout {

    public class ShellLayoutViewPresenter : Presenter<ShellLayoutView>
    {
        protected override void OnViewSet()
        {
            WorkItem.UIExtensionSites.RegisterSite(CommonUIExtensionSiteNames.MainMenu, View.MainMenuStrip);
            WorkItem.UIExtensionSites.RegisterSite(CommonUIExtensionSiteNames.MainToolbar, View.MainToolbarStrip);
        }

        /// <summary>
        /// Вызывается когда пользователь пробует закончить работу с приложением.
        /// </summary>
        public void OnFileExit()
        {
            View.ParentForm.Close();
        }

        /// <summary>
        /// Открывает вид "О программе".
        /// </summary>
		public void OnAboutClick()
        {
            using (AboutSystemBox asb = new AboutSystemBox())
            {
                asb.ShowDialog();
            }
        }


        #region Команда <<Создать>>
        /// <summary>
		/// Публиковать событие "Создать новый".
		/// </summary>
        [EventPublication(CommonEventNames.CreateNewItemFired, PublicationScope.Global)]
		public event EventHandler<System.EventArgs> NewItem;

		/// <summary>
		/// Обработчик команды "Создать".
		/// </summary>
		/// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandNames.CreateNewItem)]
		public void OnNewItem (object sender, System.EventArgs ea)
        {
			if ( NewItem != null )
			{
				NewItem (this, ea);
			}
        }
        
        #endregion

        #region Команда <<Сохранить>>
        
        /// <summary>
        /// Публиковать событие "Сохранить".
        /// </summary>
        [EventPublication(CommonEventNames.SaveItemFired, PublicationScope.Global)]
        public event EventHandler<System.EventArgs> SaveItem;

        /// <summary>
        /// Обработчик команды "Сохранить".
        /// </summary>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandNames.SaveItem)]
        public void OnSaveItem(object sender, System.EventArgs ea)
        {
            if (SaveItem != null)
            {
                SaveItem(this, EventArgs.Empty);
            }
        }
        
        #endregion

        #region Команда <<Удалить>>
        /// <summary>
        /// Публиковать глобальное событие "Дана команда "Удалить".
        /// </summary>
        [EventPublication(CommonEventNames.DeleteItemFired, PublicationScope.Global)]
        public event EventHandler<System.EventArgs> DeleteItem;

        /// <summary>
        /// Обработчик команды "Удалить".
        /// </summary>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandNames.DeleteItem)]
        public void OnDeleteItem(object sender, System.EventArgs ea)
        {
            if (DeleteItem != null)
            {
                DeleteItem(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Команда <<Обновить>>
        /// <summary>
        /// Публиковать событие "Обновить".
        /// </summary>
        [EventPublication(CommonEventNames.RefreshItemFired, PublicationScope.Global)]
        public event EventHandler<System.EventArgs> RefreshItem;

        /// <summary>
        /// Обработчик команды "Обновить".
        /// </summary>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandNames.RefreshItem)]
        public void OnRefreshItem(object sender, System.EventArgs ea)
        {
            if (RefreshItem != null)
            {
                RefreshItem(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Команда <<Экспортировать в MS Excel>>
        /// <summary>
        /// Публиковать событие "Экспортировать в MS Excel".
        /// </summary>
        [EventPublication(CommonEventNames.ExportToExcelFired, PublicationScope.Global)]
        public event EventHandler<System.EventArgs> ExportToExcel;

        /// <summary>
        /// Обработчик команды "Обновить".
        /// </summary>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandNames.ExportToExcel)]
        public void OnExportToExcel(object sender, System.EventArgs ea)
        {
            if (ExportToExcel != null)
            {
                ExportToExcel(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Команда <<Печать>>
        /// <summary>
        /// Публиковать событие "Печать".
        /// </summary>
        [EventPublication(CommonEventNames.PrintItemFired, PublicationScope.Global)]
        public event EventHandler<System.EventArgs> Print;

        /// <summary>
        /// Обработчик команды "Печать".
        /// </summary>
        /// <param name="eventArgs"></param>
        [CommandHandler(CommonCommandNames.PrintItem)]
        public void OnPrintItem(object sender, System.EventArgs ea)
        {
            if (Print != null)
            {
                Print(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
