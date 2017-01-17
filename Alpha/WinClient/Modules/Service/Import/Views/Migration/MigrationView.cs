using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System.Windows.Forms;
using Taumis.EnterpriseLibrary.Win.BaseViews.Common;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Views.Migration
{
    /// <summary>
    /// Вью формы
    /// </summary>
    [SmartPart]
    public partial class MigrationView : BaseView, IMigrationView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public MigrationView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Презентер
        /// </summary>
        /// <value>Презентер</value>
        [CreateNew]
        public new MigrationViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (MigrationViewPresenter)base.Presenter;
            }
        }

        #region Implementation of IDataMigrationView

        /// <summary>
        /// Устанавливает текущее значение в прогресс баре
        /// </summary>
        /// <param name="value">Значение прогресс бара</param>
        public void AddProgress(int value)
        {
            migrationProgressBar.Invoke(new MethodInvoker(() => migrationProgressBar.Value += value));
            StateText = string.Format("Обработано {0} из {1}", migrationProgressBar.Value, migrationProgressBar.Maximum);
        }

        /// <summary>
        /// Сбрасывает текущее значение шага
        /// </summary>
        /// <param name="maxValue">Максимальное значение интервала прогресс бара</param>
        public void ResetProgressBar(int maxValue)
        {
            migrationProgressBar.Value = 0;
            migrationProgressBar.Maximum = maxValue;
            migrationProgressBar.Step = 1;
        }

        /// <summary>
        /// Текст текущего состояния
        /// </summary>
        public string StateText
        {
            set
            {
                stateLabel.Invoke(new MethodInvoker(() => stateLabel.Text = value));
            }
        }

        #endregion

        private void goButton_Click(object sender, System.EventArgs e)
        {
            Presenter.Migrate();
            goButton.Enabled = false;
        }
    }
}