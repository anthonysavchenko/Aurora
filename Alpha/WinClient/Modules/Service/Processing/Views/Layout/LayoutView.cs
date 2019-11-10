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
        public string DirectoryPath
        {
            get
            {
                return DirectoryPathTextBox.Text;
            }
            set
            {
                DirectoryPathTextBox.Text = value;
            }
        }

        /// <summary>
        /// Результат выполнения обработки.
        /// </summary>
        public string Result
        {
            get
            {
                return ResultTextBox.Text;
            }
            set
            {
                ResultTextBox.Text = value;
            }
        }

        /// <summary>
        /// Очищает данные.
        /// </summary>
        public void ClearView()
        {
            DirectoryPathTextBox.Text = string.Empty;
            ResultTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Выбирает директорию.
        /// </summary>
        private void ChooseButton_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog _dialog = new FolderBrowserDialog();

            if (_dialog.ShowDialog() == DialogResult.OK)
            {
                DirectoryPathTextBox.Text = _dialog.SelectedPath;
            }
        }

        /// <summary>
        /// Запускает процесс переименования.
        /// </summary>
        private void ProcessButton_Click(object sender, System.EventArgs e)
        {
            Presenter.Process();
        }
    }
}