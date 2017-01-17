using System;
using System.Data;


namespace Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView
{
    /// <summary>
    /// Класс для преобразования элементов домена в DevExpress элементы вида
    /// </summary>
    public class SimpleItemViewMapper
    {
        /// <summary>
        /// Презентер
        /// </summary>
        private IBaseItemViewPresenter Presenter { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_baseItemView">Владелец презентера (вид)</param>
        public SimpleItemViewMapper(IBaseItemViewPresenterOwner _baseItemViewPresenterOwner)
        {
            Presenter = _baseItemViewPresenterOwner.Presenter;
        }

        #region Контролы System.Windows.Forms

        /// <summary>
        /// Присвоить значение контролу TextBox.
        /// </summary>
        /// <param name="_value">string</param>
        /// <param name="_textBox"></param>
        public void DomainToView(string _value, System.Windows.Forms.TextBox _textBox)
        {
            _textBox.Text = _value;
        }

        /// <summary>
        /// Возвращает значение контрола TextBox.
        /// </summary>
        /// <param name="_textBox"></param>
        /// <returns></returns>
        public string ViewToDomain(System.Windows.Forms.TextBox _textBox)
        {
            return _textBox.Text;
        }

        /// <summary>
        /// Присвоить значение контролу TextBox.
        /// </summary>
        /// <param name="_value">string</param>
        /// <param name="_textBox"></param>
        public void DomainToView(string _value, System.Windows.Forms.MaskedTextBox _textBox)
        {
            _textBox.Text = _value;
        }

        /// <summary>
        /// Возвращает значение контрола TextBox.
        /// </summary>
        /// <param name="_textBox"></param>
        /// <returns></returns>
        public string ViewToDomain(System.Windows.Forms.MaskedTextBox _textBox)
        {
            return _textBox.Text;
        }

        /// <summary>
        /// Присвоить значение контролу Label.
        /// </summary>
        /// <param name="_value">string</param>
        /// <param name="_labelText"></param>
        public void DomainToView(string _value, System.Windows.Forms.Label _labelText)
        {
            _labelText.Text = _value;
        }

        /// <summary>
        /// Возвращает значение контрола Label.
        /// </summary>
        /// <param name="_labelText"></param>
        /// <returns></returns>
        public string ViewToDomain(System.Windows.Forms.Label _labelText)
        {
            return _labelText.Text;
        }

        /// <summary>
        /// Возвращает значение контрола DateTimePicker.
        /// </summary>
        /// <param name="_labelText"></param>
        /// <returns></returns>
        public DateTime ViewToDomain(System.Windows.Forms.DateTimePicker _dateTimePicker)
        {
            DateTime _dateTime = DateTime.MinValue;

            if (_dateTimePicker.Checked)
            {
                _dateTime = _dateTimePicker.Value;
            }

            return _dateTime;
        }

        /// <summary>
        /// Присвоить значение контролу DateTimePicker.
        /// </summary>
        /// <param name="_value">DateTime</param>
        /// <param name="_labelText"></param>
        public void DomainToView(DateTime _value, System.Windows.Forms.DateTimePicker _dateTimePicker)
        {
            if (_value != DateTime.MinValue)
            {
                _dateTimePicker.Checked = true;
                _dateTimePicker.Value = _value;
            }
            else
            {
                _dateTimePicker.Checked = false;
            }
        }

        /// <summary>
        /// Присвоить значение контролу CheckBox.
        /// </summary>
        /// <param name="_value">bool</param>
        /// <param name="_checkBox"></param>
        public void DomainToView(bool _value, System.Windows.Forms.CheckBox _checkBox)
        {
            _checkBox.Checked = _value;
        }

        /// <summary>
        /// Возвращает значение контрола CheckBox.
        /// </summary>
        /// <param name="_checkBox"></param>
        /// <returns></returns>
        public bool ViewToDomain(System.Windows.Forms.CheckBox _checkBox)
        {
            return _checkBox.Checked;
        }

        /// <summary>
        /// Присвоить значение контролу RadioButton.
        /// </summary>
        /// <param name="_value">bool</param>
        /// <param name="_checkBox"></param>
        public void DomainToView(bool _value, System.Windows.Forms.RadioButton _radioButton)
        {
            _radioButton.Checked = _value;
        }

        /// <summary>
        /// Возвращает значение контрола RadioButton.
        /// </summary>
        /// <param name="_checkBox"></param>
        /// <returns></returns>
        public bool ViewToDomain(System.Windows.Forms.RadioButton _radioButton)
        {
            return _radioButton.Checked;
        }

#endregion

        #region Контролы DevExpress.XtraEditors

        /// <summary>
        /// Присвоить значение DataTable контролу LookUpEdit.
        /// </summary>
        /// <param name="_dataTable">DataTable</param>
        /// <param name="_lookupEdit"></param>
        public void DomainToView(DataTable _dataTable, DevExpress.XtraEditors.LookUpEdit _lookupEdit)
        {
            _lookupEdit.Properties.DataSource = _dataTable;
            _lookupEdit.Properties.ForceInitialize();
        }

        /// <summary>
        /// Присвоить значение Домена контролу LookUpEdit.
        /// </summary>
        /// <param name="_domain">Домен</param>
        /// <param name="_lookupEdit"></param>
        public void DomainToView(DomainObject _domain, DevExpress.XtraEditors.LookUpEdit _lookupEdit)
        {
            if (_domain != null)
            {
                ((System.ComponentModel.ISupportInitialize)(_lookupEdit.Properties)).BeginInit();
                _lookupEdit.Properties.ValueMember = "ID";
                _lookupEdit.EditValue = _domain.ID;
                ((System.ComponentModel.ISupportInitialize)(_lookupEdit.Properties)).EndInit();
            }
            else
            {
                _lookupEdit.EditValue = null;
            }
        }

        /// <summary>
        /// Присвоить значение контролу LookUpEdit.
        /// </summary>
        /// <param name="_value">Значение</param>
        /// <param name="_lookupEdit">Контрол</param>
        public void DomainToView(string _value, DevExpress.XtraEditors.LookUpEdit _lookupEdit)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                ((System.ComponentModel.ISupportInitialize)(_lookupEdit.Properties)).BeginInit();
                _lookupEdit.EditValue = _value;
                ((System.ComponentModel.ISupportInitialize)(_lookupEdit.Properties)).EndInit();
            }
            else
            {
                _lookupEdit.EditValue = null;
            }
        }

        /// <summary>
        /// Возвращает значение, выбранное в контроле LookUpEdit, в виде домена
        /// </summary>
        /// <typeparam name="TDomain">Домен</typeparam>
        /// <param name="_lookupEdit">Контрол</param>
        /// <returns>Домен</returns>
        public TDomain ViewToDomain<TDomain>(DevExpress.XtraEditors.LookUpEdit _lookupEdit)
            where TDomain : DomainObject
        {
            TDomain _res = null;

            if (_lookupEdit.ItemIndex != -1)
            {
                string _id = _lookupEdit.GetColumnValue("ID").ToString();
                _res = Presenter.GetItem<TDomain>(_id);
            }

            return _res;
        }

        /// <summary>
        /// Возвращает значение, выбранное в контроле LookUpEdit, в виде строки
        /// </summary>
        /// <param name="_lookupEdit">Контрол</param>
        /// <returns>Строковое значение</returns>
        public string ViewToDomain(DevExpress.XtraEditors.LookUpEdit _lookupEdit)
        {
            return (string)_lookupEdit.EditValue;
        }

        /// <summary>
        /// Присвоить значение контролу TextEdit.
        /// </summary>
        /// <param name="_value">string</param>
        /// <param name="_textBox"></param>
        public void DomainToView(string _value, DevExpress.XtraEditors.TextEdit _textBox)
        {
            _textBox.Text = _value;
        }

        /// <summary>
        /// Возвращает значение контрола TextEdit.
        /// </summary>
        /// <param name="_textBox"></param>
        public string ViewToDomain(DevExpress.XtraEditors.TextEdit _textBox)
        {
            return _textBox.Text;
        }

        /// <summary>
        /// Присвоить значение контролу DateEdit.
        /// </summary>
        /// <param name="_value">DateTime</param>
        /// <param name="_textBox"></param>
        public void DomainToView(DateTime _value, DevExpress.XtraEditors.DateEdit _dateEdit)
        {
            _dateEdit.EditValue = _value;
        }

        /// <summary>
        /// Возвращает значение контрола DateEdit.
        /// </summary>
        /// <param name="_textBox"></param>
        public DateTime ViewToDomain(DevExpress.XtraEditors.DateEdit _dateEdit)
        {
            return _dateEdit.DateTime;
        }

        /// <summary>
        /// Присвоить значение контролу SpinEdit.
        /// </summary>
        /// <param name="_value">decimal</param>
        /// <param name="_textBox"></param>
        public void DomainToView<T>(T _value, DevExpress.XtraEditors.SpinEdit _spinEdit)
        {
            _spinEdit.EditValue = _value;
        }

        /// <summary>
        /// Возвращает значение контрола SpinEdit.
        /// </summary>
        /// <param name="_textBox"></param>
        public object ViewToDomain(DevExpress.XtraEditors.SpinEdit _spinEdit)
        {
            return _spinEdit.EditValue;
        }

        /// <summary>
        /// Присвоить значение контролу SpinEdit.
        /// </summary>
        /// <param name="_value">int</param>
        /// <param name="_textBox"></param>
        public void DomainToView(int _value, DevExpress.XtraEditors.SpinEdit _spinEdit)
        {
            _spinEdit.EditValue = _value;
        }

        /// <summary>
        /// Присвоить значение контролу SpinEdit.
        /// </summary>
        /// <param name="_value">float</param>
        /// <param name="_textBox"></param>
        public void DomainToView(float _value, DevExpress.XtraEditors.SpinEdit _spinEdit)
        {
            _spinEdit.EditValue = (decimal)_value;
        }

        /// <summary>
        /// Присвоить значение контролу LabelControl.
        /// </summary>
        /// <param name="_value">string</param>
        /// <param name="_labelText"></param>
        public void DomainToView(string _value, DevExpress.XtraEditors.LabelControl _labelText)
        {
            _labelText.Text = _value;
        }

        /// <summary>
        /// Возвращает значение контрола LabelControl.
        /// </summary>
        /// <param name="_labelText"></param>
        /// <returns></returns>
        public string ViewToDomain(DevExpress.XtraEditors.LabelControl _labelText)
        {
            return _labelText.Text;
        }

        /// <summary>
        /// Присвоить значение контролу MaskBox.
        /// </summary>
        /// <param name="_value">string</param>
        /// <param name="_labelText"></param>
        public void DomainToView(string _value, DevExpress.XtraEditors.Mask.MaskBox _maskBox)
        {
            _maskBox.EditValue = _value;
        }

        /// <summary>
        /// Возвращает значение контрола MaskBox.
        /// </summary>
        /// <param name="_labelText"></param>
        /// <returns></returns>
        public string ViewToDomain(DevExpress.XtraEditors.Mask.MaskBox _maskBox)
        {
            return (string)_maskBox.EditValue;
        }

        /// <summary>
        /// Присвоить значение контролу CheckEdit.
        /// </summary>
        /// <param name="_value">bool</param>
        /// <param name="CheckEdit"></param>
        public void DomainToView(bool _value, DevExpress.XtraEditors.CheckEdit checkEdit)
        {
            checkEdit.Checked = _value;
        }

        /// <summary>
        /// Возвращает значение контрола CheckEdit.
        /// </summary>
        /// <param name="CheckEdit"></param>
        /// <returns></returns>
        public bool ViewToDomain(DevExpress.XtraEditors.CheckEdit checkEdit)
        {
            return checkEdit.Checked;
        }

        /// <summary>
        /// Присвоить значение контролу ColorEdit.
        /// </summary>
        /// <param name="_value">Численное представление цвета</param>
        public void DomainToView(int _value, DevExpress.XtraEditors.ColorEdit _colorEdit)
        {
            _colorEdit.EditValue = System.Drawing.Color.FromArgb(_value);
        }

        /// <summary>
        /// Возвращает значение контрола ColorEdit.
        /// </summary>
        /// <returns>Численное представление цвета</returns>
        public int ViewToDomain(DevExpress.XtraEditors.ColorEdit _colorEdit)
        {
            return ((System.Drawing.Color)_colorEdit.EditValue).ToArgb();
        }

        #endregion
    }
}