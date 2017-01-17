using System;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using Taumis.EnterpriseLibrary.Win.Constants;
using Status = Microsoft.Practices.CompositeUI.Commands.CommandStatus;

namespace Taumis.Infrastructure.Layout
{
	[SmartPart]
	public partial class ShellLayoutView : UserControl
    {
        private ShellLayoutViewPresenter _presenter;

        /// <summary>
        /// Инициализация класса главного вида оболочки <see cref="T:ShellLayoutView"/>.
        /// </summary>
        public ShellLayoutView()
        {
            InitializeComponent();
			this.Dock = DockStyle.Fill;
		}

		protected override void OnLoad (EventArgs e) {
			base.OnLoad (e);
			// Обработчики команды "Создать".
			this._presenter.WorkItem.Commands[CommonCommandNames.CreateNewItem].AddInvoker (this.btnNew, "Click");
			this._presenter.WorkItem.Commands[CommonCommandNames.CreateNewItem].AddInvoker (this._newToolStripMenuItem, "Click");
			this._presenter.WorkItem.Commands[CommonCommandNames.CreateNewItem].Status = Status.Disabled;

			// Обработчик команды "Сохранить".
			this._presenter.WorkItem.Commands[CommonCommandNames.SaveItem].AddInvoker (this.btnSave, "Click");
			this._presenter.WorkItem.Commands[CommonCommandNames.SaveItem].AddInvoker (this._saveToolStripMenuItem, "Click");
			this._presenter.WorkItem.Commands[CommonCommandNames.SaveItem].Status = Status.Disabled;

			// Обработчик команды "Удалить".
			this._presenter.WorkItem.Commands[CommonCommandNames.DeleteItem].AddInvoker (this.btnDelete, "Click");
			this._presenter.WorkItem.Commands[CommonCommandNames.DeleteItem].AddInvoker (this._deleteToolStripMenuItem, "Click");
			this._presenter.WorkItem.Commands[CommonCommandNames.DeleteItem].Status = Status.Disabled;

            // Запуск команды "Обновить".
            this._presenter.WorkItem.Commands[CommonCommandNames.RefreshItem].AddInvoker(this.btnRefresh, "Click");
            this._presenter.WorkItem.Commands[CommonCommandNames.RefreshItem].AddInvoker(this._refreshListToolStripMenuItem, "Click");
            this._presenter.WorkItem.Commands[CommonCommandNames.RefreshItem].Status = Status.Disabled;

            // Запуск команды "Экспортировать в MS Excel".
            this._presenter.WorkItem.Commands[CommonCommandNames.ExportToExcel].AddInvoker(this.btnExportToExcel, "Click");
            this._presenter.WorkItem.Commands[CommonCommandNames.ExportToExcel].AddInvoker(this._exportToExcelToolStripMenuItem, "Click");
            this._presenter.WorkItem.Commands[CommonCommandNames.ExportToExcel].Status = Status.Disabled;

            // Запуск команды "Печать".
            this._presenter.WorkItem.Commands[CommonCommandNames.PrintItem].AddInvoker(this.btnPrint, "Click");
            this._presenter.WorkItem.Commands[CommonCommandNames.PrintItem].AddInvoker(this._printToolStripMenuItem, "Click");
            this._presenter.WorkItem.Commands[CommonCommandNames.PrintItem].Status = Status.Disabled;
        
        }
        
		/// <summary>
        /// Презентер
        /// </summary>
        [CreateNew]
        public ShellLayoutViewPresenter Presenter
        {
            set
            {
                _presenter = value;
                _presenter.View = this;
            }
        }

        /// <summary>
        /// Главное меню системы.
        /// </summary>
        /// <value>Главное меню системы.</value>
        internal MenuStrip MainMenuStrip
        {
            get { return _mainMenuStrip; }
        }

        /// <summary>
        /// Главная инструментальная панель системы.
        /// </summary>
        /// <value>Главная инструментальная панель системы.</value>
        internal ToolStrip MainToolbarStrip
        {
            get { return _mainToolStrip; }
        }

        /// <summary>
        /// Завершить программу.
        /// </summary>
        private void OnFileExit(object sender, EventArgs e)
        {
            _presenter.OnFileExit();
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            _presenter.OnAboutClick();
        }
    }
}
