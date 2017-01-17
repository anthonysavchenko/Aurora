using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Export
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
        /// Учетный период
        /// </summary>
        public DateTime Period
        {
            set
            {
                PeriodDateEdit.DateTime = value;
            }
            get
            {
                return PeriodDateEdit.DateTime;
            }
        }

        public string BenefitInputFilePath
        {
            get { return benefitInputFileTextEdit.Text; }
        }

        /// <summary>
        /// Флаг экспорта данных ГИС ЖКХ: "только новые" / "все" абоненты
        /// </summary>
        public bool GisZhkhOnlyNew
        {
            get
            {
                return (bool)gisZhkhOnlyNewRadioGroup.EditValue;
            }
            set
            {
                gisZhkhOnlyNewRadioGroup.EditValue = value;
            }
        }

        /// <summary>
        /// Путь к файлу шаблона
        /// </summary>
        public string GisZhkhInputFilePath
        {
            get
            {
                return gisZhkhInputFileTextEdit.Text;
            }
        }

        public void ShowBenefitProgressBar()
        {
            benefitExportBtn.Enabled = false;
            benefitProgressBar.Visible = true;
        }

        public void HideBenefitProgressBar()
        {
            benefitExportBtn.Enabled = true;
            benefitProgressBar.Visible = false;
        }

        public void ShowGisZhkhProgressBar()
        {
            gisZhkhExportButton.Enabled = false;
            gisZhkhProgressBar.Visible = true;
        }

        public void HideGisZhkhProgressBar()
        {
            gisZhkhExportButton.Enabled = true;
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
        /// Экспортировать файл
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <param name="e">Аргументы</param>
        private void ExportButton_Click(object sender, System.EventArgs e)
        {
            Presenter.ExportFile();
        }

        private void benefitExportBtn_Click(object sender, EventArgs e)
        {
            Presenter.BenefitExport();
        }

        private void benefitInputSelectFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Application.StartupPath + @"\Data",
                Title = "Открыть файл",
                Filter = "Книга Microsoft Excel 97-2003 (*.xls)|*.xls|Книга Microsoft Excel 2007 (*.xlsx)|*.xlsx",
                FilterIndex = 1,
                DefaultExt = "xls",
                RestoreDirectory = true,
            };

            benefitInputFileTextEdit.Text = _openFileDialog.ShowDialog() == DialogResult.OK
                ? _openFileDialog.FileName
                : string.Empty;
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

        private void gisZhkhExportButton_Click(object sender, EventArgs e)
        {
            Presenter.GisZhkhExport();
        }
    }
}