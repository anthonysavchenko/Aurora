using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using System.Windows.Forms;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using RefBook = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
//using BaseLayoutView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    /// <summary>
    /// Вью формы
    /// </summary>
    [SmartPart]
    public partial class LayoutView : BaseLayoutView, ILayoutView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public LayoutView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Презентер
        /// </summary>
        /// <value>Презентер</value>
        [CreateNew]
        public new LayoutViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (LayoutViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Полное имя файла
        /// </summary>
        public string FilePath
        {
            set
            {
                FileTextEdit.Text = value;
            }
            get
            {
                return FileTextEdit.Text;
            }
        }

        /// <summary>
        /// Полное имя файла для импорта абонентов
        /// </summary>
        public string ImportCustomersFilePath
        {
            set
            {
                ImportCustomersFileTextEdit.Text = value;
            }
            get
            {
                return ImportCustomersFileTextEdit.Text;
            }
        }

        /// <summary>
        /// Услуга
        /// </summary>
        public RefBook.Service Service
        {
            get
            {
                RefBook.Service _res = null;

                if (ServiceLookUpEdit.ItemIndex != -1)
                {
                    string _id = ServiceLookUpEdit.GetColumnValue("ID").ToString();
                    _res = Presenter.GetItem<RefBook.Service>(_id);
                }

                return _res;
            }
        }

        /// <summary>
        /// Подрядчик
        /// </summary>
        public Contractor Contractor
        {
            get
            {
                Contractor _res = null;

                if (ContractorLookUpEdit.ItemIndex != -1)
                {
                    string _id = ContractorLookUpEdit.GetColumnValue("ID").ToString();
                    _res = Presenter.GetItem<Contractor>(_id);
                }

                return _res;
            }
        }

        /// <summary>
        /// Тариф
        /// </summary>
        public decimal Rate
        {
            get
            {
                return (decimal)RateSpinEdit.EditValue;
            }
        }

        /// <summary>
        /// Только для квартир в собственности
        /// </summary>
        public bool IsPrivate
        {
            get
            {
                return IsPrivateCheckEdit.Checked;
            }
        }

        /// <summary>
        /// Услуги
        /// </summary>
        public DataTable Services
        {
            set
            {
                ServiceLookUpEdit.Properties.DataSource = value;
                ServiceLookUpEdit.Properties.ForceInitialize();
            }
        }

        /// <summary>
        /// Подрядчики
        /// </summary>
        public DataTable Contractors
        {
            set
            {
                ContractorLookUpEdit.Properties.DataSource = value;
                ContractorLookUpEdit.Properties.ForceInitialize();
            }
        }

        /// <summary>
        /// Путь к файлу шаблона
        /// </summary>
        public string GisZhkhInputFilePath {
            get
            {
                return gisZhkhInputFileTextEdit.Text;
            }
        }

        public void ShowGisZhkhProgressBar()
        {
            gisZhkhImportButton.Enabled = false;
            gisZhkhProgressBar.Visible = true;
        }

        public void HideGisZhkhProgressBar()
        {
            gisZhkhImportButton.Enabled = true;
            gisZhkhProgressBar.Visible = false;
        }

        /// <summary>
        /// Найти файл
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Аргументы</param>
        private void BrowseButton_Click(object sender, System.EventArgs e)
        {
            Presenter.FindFile();
        }

        /// <summary>
        /// Импортировать файл
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Аргументы</param>
        private void ImportButton_Click(object sender, System.EventArgs e)
        {
            Presenter.ImportFile();
        }

        /// <summary>
        /// Добавить услугу и тариф всем абонентам
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddServicesForCustomersButton_Click(object sender, EventArgs e)
        {
            Presenter.CreateServicesForCustomers();
        }

        /// <summary>
        /// Находит файл для импорта абонентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportCustomersBrowseButton_Click(object sender, EventArgs e)
        {
            Presenter.ImportCustomersFindFile();
        }

        /// <summary>
        /// Импортирует файл для импорта абонентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportCustomersImportButton_Click(object sender, EventArgs e)
        {
            Presenter.ImportCustomersImport();
        }

        private void gisZhkhSelectInputFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath + @"\Data",
                Title = "Открыть файл",
                Filter = "Книга Microsoft Excel (*.xls;*.xlsx)|*.xls;*.xlsx",
                FilterIndex = 0,
                DefaultExt = "xlsx",
                RestoreDirectory = true,
            };

            gisZhkhInputFileTextEdit.Text = _openFileDialog.ShowDialog() == DialogResult.OK
                ? _openFileDialog.FileName
                : string.Empty;
        }

        private void gisZhkhImportButton_Click(object sender, EventArgs e)
        {
            Presenter.GisZhkhImport();
        }
    }
}