using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Windows.Forms;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Layout;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
//using BaseLayoutView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Processing.Views.Layout
{
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
        /// Путь к директории для переименования файлов.
        /// </summary>
        public string DirectoryPathForRename
        {
            get
            {
                return DirectoryPathForRenameTextBox.Text;
            }
            set
            {
                DirectoryPathForRenameTextBox.Text = value;
            }
        }

        /// <summary>
        /// Путь к директории для анализа файлов.
        /// </summary>
        public string DirectoryPathForAnalyze
        {
            get
            {
                return DirectoryPathForAnalyzeTextBox.Text;
            }
            set
            {
                DirectoryPathForAnalyzeTextBox.Text = value;
            }
        }

        /// <summary>
        /// Результат выполнения обработки.
        /// </summary>
        public string Result
        {
            set
            {
                ResultTextBox.AppendText(value + "\r\n");
            }
        }

        /// <summary>
        /// Очищает данные.
        /// </summary>
        public void ClearView()
        {
            DirectoryPathForRenameTextBox.Text = string.Empty;
            DirectoryPathForAnalyzeTextBox.Text = string.Empty;
            ResultTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Выбирает директорию.
        /// </summary>
        private void ChooseForRenameButton_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog _dialog = new FolderBrowserDialog();

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryPathForRenameTextBox.Text = _dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Выбирает директорию для анализа файлов.
        /// </summary>
        private void ChooseForAnalyzeButton_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog _dialog = new FolderBrowserDialog();

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryPathForAnalyzeTextBox.Text = _dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Запускает процесс переименования.
        /// </summary>
        private void RenameButton_Click(object sender, System.EventArgs e)
        {
            Presenter.Rename();
        }

        /// <summary>
        /// Запускает процесс анализа.
        /// </summary>
        private void AnalyzeButton_Click(object sender, System.EventArgs e)
        {
            Presenter.Analyze();
        }
    }
}