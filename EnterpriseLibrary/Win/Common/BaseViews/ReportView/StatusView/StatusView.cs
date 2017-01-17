using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.EnterpriseLibrary.Win.BaseViews.ReportView.StatusView
{
    /// <summary>
    /// Вид с бегущим прогрессбаром для отчетов
    /// </summary>
    [SmartPart]
    public partial class StatusView : BaseView, IStatusView
    {
        public StatusView()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        [CreateNew]
        public new  StatusViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
        }

        /// <summary>
        /// Запускает бегущую строки
        /// </summary>
        public void StartMarqueProgress()
        {
            _statusProgressBar.Visible = true;
            _statusProgressBar.Style = ProgressBarStyle.Marquee;
            _statusProgressBar.Value = 0;
        }

        /// <summary>
        /// Останавливает бегущую строки
        /// </summary>
        public void StopMarqueProgress()
        {
            _statusProgressBar.Visible = false;
            _statusProgressBar.Style = ProgressBarStyle.Blocks;
            _statusProgressBar.Value = 0;
        }
    }
}
