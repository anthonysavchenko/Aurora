using System;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Reports.DebtRepayment;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers.Views
{
    public partial class CreateDebtPaymentAgreementBox : Form
    {
        Customer _customer;

        public CreateDebtPaymentAgreementBox(Customer customer)
        {
            InitializeComponent();
            _customer = customer;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            DebtRepaymentDataSet _ds = new DebtRepaymentDataSet();
            _ds.Customers.Rows.Add(
                DateTime.Now,
                _customer.PhysicalPersonShortName,
                _customer.PhysicalPersonFullName,
                $"Общество с ограниченной ответственностью «Управляющая компания Фрунзенского района», далее именуемое Кредитор, в лице директора Слаутенко Анатолия Васильевича, действующего на основании Устава, и {_customer.PhysicalPersonFullName}, собственником квартиры № {_customer.Apartment} в доме № {_customer.Building.Number} по ул. {_customer.Building.Street.Name} в г. Владивостоке, далее именуемый Должник, с другой стороны, заключили настоящее соглашение о нижеследующем:",
                ValueTextEdit.Text.ToString() + " руб.",
                UntilDateEdit.Text.ToString(),
                MonthlyPaymentTextEdit.Text.ToString() + " руб.",
                PasportSeriesTextEdit.Text,
                PasportNumberTextEdit.Text,
                PasportFromTextBox.Text,
                PasportSinceDateEdit.Text,
                PhoneTextEdit.Text);

            DebtRepaymentReportObject _doc = new DebtRepaymentReportObject();
            _doc.DataSource = _ds;

            ReportPrintTool _reportPrintTool = new ReportPrintTool(_doc);
            _reportPrintTool.ShowPreviewDialog();

            Close();
        }
    }
}
