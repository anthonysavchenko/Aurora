using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using Microsoft.Practices.CompositeUI;
using System;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Taumis.EnterpriseLibrary.Win.Services
{
    /// <summary>
    /// Сервис контроля изменений значений контролов
    /// </summary>
    [Service(typeof(IChangeEventHandlerService))]
    public class ChangeEventHandlerService : IChangeEventHandlerService
    {
        /// <summary>
        /// Добавить в коллекцию элементов управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_collection">Коллекция элементов управления</param>
        /// <param name="_handler">Обработчик</param>
        public void Bind(Control.ControlCollection _collection, EventHandler _handler)
        {
            foreach (Control _control in _collection)
            {
                if (_control.Controls.Count != 0)
                {
                    Bind(_control.Controls, _handler);
                }

                Bind(_control, _handler);
            }
        }

        /// <summary>
        /// Убрать из коллекции элементов управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_collection">Коллекция элементов управления</param>
        /// <param name="_handler">Обработчик</param>
        public void UnBind(Control.ControlCollection _collection, EventHandler _handler)
        {
            foreach (Control _control in _collection)
            {
                if (_control.Controls.Count != 0)
                {
                    UnBind(_control.Controls, _handler);
                }

                UnBind(_control, _handler);
            }
        }

        /// <summary>
        /// Добавить в элемент управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_control">Элемент управления</param>
        /// <param name="_handler">Обработчик</param>
        public void Bind(Control _control, EventHandler _handler)
        {
            /* System.Windows.Forms */
            if (_control.GetType() == typeof(TextBox))
            {
                (_control as TextBox).TextChanged += _handler;
            }
            else if (_control.GetType() == typeof(DateTimePicker))
            {
                (_control as DateTimePicker).ValueChanged += _handler;
            }
            else if (_control.GetType() == typeof(CheckBox))
            {
                (_control as CheckBox).CheckedChanged += _handler;
            }
            else if (_control.GetType() == typeof(MaskedTextBox))
            {
                (_control as MaskedTextBox).TextChanged += _handler;
            }
            else if (_control.GetType() == typeof(RadioButton))
            {
                (_control as RadioButton).CheckedChanged += _handler;
            }
            else if (_control.GetType() == typeof(ComboBox))
            {
                (_control as ComboBox).SelectedValueChanged += _handler;
            }
            else if (_control.GetType() == typeof(NumericUpDown))
            {
                ((NumericUpDown)_control).ValueChanged += _handler;
            }

            /* DevExpress */
            else if (_control.GetType() == typeof(LookUpEdit))
            {
                (_control as LookUpEdit).EditValueChanged += _handler;
            }
            else if (_control.GetType() == typeof(SpinEdit))
            {
                (_control as SpinEdit).ValueChanged += _handler;
            }
            else if (_control.GetType() == typeof(ComboBoxEdit))
            {
                (_control as ComboBoxEdit).SelectedValueChanged += _handler;
            }
            else if (_control.GetType() == typeof(TextEdit))
            {
                (_control as TextEdit).TextChanged += _handler;
            }
            else if (_control.GetType() == typeof(ColorEdit))
            {
                (_control as ColorEdit).EditValueChanged += _handler;
            }
            // Если поле ввода TextBoxMaskBox и не является дочерним контролом контрола типа LookUpEdit, 
            // иначе будем получать лишние события - событие изменения TextBoxMaskBox при снятии фокуса с LookUpEdit.
            else if (
                _control.GetType() == typeof(TextBoxMaskBox) &&
                _control.Parent.GetType() != typeof(LookUpEdit) &&
                _control.Parent.GetType() != typeof(SpinEdit) &&
                _control.Parent.GetType() != typeof(ComboBoxEdit))
            {
                (_control as TextBoxMaskBox).TextChanged += _handler;
            }
            else if (_control.GetType() == typeof(GridControl))
            {
                foreach (RepositoryItem item in (_control as GridControl).RepositoryItems)
                {
                    item.EditValueChanged += _handler;
                }
            }
            else if (_control.GetType() == typeof(TrackBar))
            {
                ((TrackBar)_control).ValueChanged += _handler;
            }
            else if (_control.GetType() == typeof (CheckEdit))
            {
                ((CheckEdit)_control).CheckedChanged += _handler;
            }
        }

        /// <summary>
        /// Убрать из элемента управления обработчик изменения введенного значения
        /// </summary>
        /// <param name="_control">Элемент управления</param>
        /// <param name="_handler">Обработчик</param>
        public void UnBind(Control _control, EventHandler _handler)
        {
            /* System.Windows.Forms */
            if (_control.GetType() == typeof(TextBox))
            {
                (_control as TextBox).TextChanged -= _handler;
            }
            else if (_control.GetType() == typeof(DateTimePicker))
            {
                (_control as DateTimePicker).ValueChanged -= _handler;
            }
            else if (_control.GetType() == typeof(CheckBox))
            {
                (_control as CheckBox).CheckedChanged -= _handler;
            }
            else if (_control.GetType() == typeof(MaskedTextBox))
            {
                (_control as MaskedTextBox).TextChanged -= _handler;
            }
            else if (_control.GetType() == typeof(RadioButton))
            {
                (_control as RadioButton).CheckedChanged -= _handler;
            }
            else if (_control.GetType() == typeof(ComboBox))
            {
                (_control as ComboBox).SelectedValueChanged -= _handler;
            }
            else if (_control.GetType() == typeof(NumericUpDown))
            {
                ((NumericUpDown)_control).ValueChanged -= _handler;
            }

            /* DevExpress */
            else if (_control.GetType() == typeof(LookUpEdit))
            {
                (_control as LookUpEdit).EditValueChanged -= _handler;
            }
            else if (_control.GetType() == typeof(SpinEdit))
            {
                (_control as SpinEdit).ValueChanged -= _handler;
            }
            else if (_control.GetType() == typeof(ComboBoxEdit))
            {
                (_control as ComboBoxEdit).SelectedValueChanged -= _handler;
            }
            else if (_control.GetType() == typeof(TextEdit))
            {
                (_control as TextEdit).TextChanged -= _handler;
            }
            else if (_control.GetType() == typeof(ColorEdit))
            {
                (_control as ColorEdit).EditValueChanged -= _handler;
            }
            else if (_control.GetType() == typeof(TextBoxMaskBox))
            {
                (_control as TextBoxMaskBox).TextChanged -= _handler;
            }
            else if (_control.GetType() == typeof(GridControl))
            {
                foreach (RepositoryItem item in (_control as GridControl).RepositoryItems)
                {
                    item.EditValueChanged -= _handler;
                }
            }
            else if (_control.GetType() == typeof(TrackBar))
            {
                ((TrackBar)_control).ValueChanged -= _handler;
            }
            else if (_control.GetType() == typeof(CheckEdit))
            {
                ((CheckEdit)_control).CheckedChanged -= _handler;
            }
        }
    }
}
