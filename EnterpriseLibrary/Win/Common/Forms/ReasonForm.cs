using System.Windows.Forms;

namespace Taumis.EnterpriseLibrary.Win.Forms
{
    /// <summary>
    /// Окно причины выполнения действия
    /// </summary>
    public partial class ReasonForm : Form
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ReasonForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Caption
        {
            set
            {
                Text = value;
            }
            get
            {
                return Text;
            }
        }

        /// <summary>
        /// Причина
        /// </summary>
        public string Reason
        {
            set
            {
                _reasonText.Text = value;
            }
            get
            {
                return _reasonText.Text;
            }
        }

        /// <summary>
        /// Обработчик события на изменение текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _reasonText_TextChanged(object sender, System.EventArgs e)
        {
            _btnOk.Enabled = _reasonText.Text.Length > 0;
        }
    }
}
