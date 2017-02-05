using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.ObjectBuilder;
using System;
using System.Data;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseItemView;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc.Customer;

//using BaseItemView = System.Windows.Forms.UserControl;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Customers
{
    /// <summary>
    /// Вид деталей
    /// </summary>
    [SmartPart]
    public partial class ItemView : BaseItemView, IItemView
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ItemView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Создание презентера для формы
        /// </summary>
        [CreateNew]
        public new ItemViewPresenter Presenter
        {
            set
            {
                base.Presenter = value;
            }
            get
            {
                return (ItemViewPresenter)base.Presenter;
            }
        }

        /// <summary>
        /// Улицы
        /// </summary>
        public DataTable Streets
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, streetLookUpEdit);
            }
        }

        /// <summary>
        /// Дома
        /// </summary>
        public DataTable Buildings
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, buildingLookUpEdit);
            }
        }

        /// <summary>
        /// Тип собственника
        /// </summary>
        public DomItem.OwnerTypes OwnerType
        {
            set
            {
                switch (value)
                {
                    case DomItem.OwnerTypes.PhysicalPerson:
                        PhysicalPersonRadioButton.Checked = true;
                        break;
                    case DomItem.OwnerTypes.JuridicalPerson:
                        JuridicalPersonRadioButton.Checked = true;
                        break;
                    default:
                        UnknownRadioButton.Checked = true;
                        break;
                }
            }
            get
            {
                DomItem.OwnerTypes _value = DomItem.OwnerTypes.Unknown;

                if (PhysicalPersonRadioButton.Checked)
                {
                    _value = DomItem.OwnerTypes.PhysicalPerson;
                }
                else if (JuridicalPersonRadioButton.Checked)
                {
                    _value = DomItem.OwnerTypes.JuridicalPerson;
                }

                return _value;
            }
        }

        /// <summary>
        /// Полное имя физического лица
        /// </summary>
        public string PhysicalPersonFullName
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, PhysicalPersonFullNameTextEdit);
            }
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(PhysicalPersonFullNameTextEdit);
            }
        }

        /// <summary>
        /// Краткое имя физического лица
        /// </summary>
        public string PhysicalPersonShortName
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, PhysicalPersonShortNameTextEdit);
            }
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(PhysicalPersonShortNameTextEdit);
            }
        }

        /// <summary>
        /// Полное наименование юридического лица
        /// </summary>
        public string JuridicalPersonFullName
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, JuridicalPersonFullNameTextEdit);
            }
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(JuridicalPersonFullNameTextEdit);
            }
        }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public string Account
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, AccountTextEdit);
            }
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(AccountTextEdit);
            }
        }

        /// <summary>
        /// Улица
        /// </summary>
        public Street Street
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain<Street>(streetLookUpEdit);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, streetLookUpEdit);
            }
        }

        /// <summary>
        /// Дом
        /// </summary>
        public Building Building
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain<Building>(buildingLookUpEdit);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, buildingLookUpEdit);
            }
        }

        /// <summary>
        /// Этаж
        /// </summary>
        public short Floor
        {
            get
            {
                return Convert.ToInt16(floorSpinEdit.Value);
            }
            set
            {
                floorSpinEdit.Value = value;
            }
        }

        /// <summary>
        /// Подъезд
        /// </summary>
        public byte Entrance
        {
            get
            {
                return Convert.ToByte(entranceNumericUpDown.Value);
            }
            set
            {
                entranceNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Последний этаж
        /// </summary>
        public short FloorMax
        {
            set
            {
                floorSpinEdit.Maximum = value;
            }
        }

        /// <summary>
        /// Последний подъезд
        /// </summary>
        public byte EntranceMax
        {
            set
            {
                entranceNumericUpDown.Maximum = value;
            }
        }

        /// <summary>
        /// Квартира
        /// </summary>
        public string Apartment
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(ApartmentTextEdit);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, ApartmentTextEdit);
            }
        }

        /// <summary>
        /// Площадь
        /// </summary>
        public decimal Square
        {
            get
            {
                return areaNumericUpDown.Value;
            }
            set
            {
                areaNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Общая площадь
        /// </summary>
        public decimal HeatedArea
        {
            get
            {
                return heatedAreaNumericUpDown.Value;
            }
            set
            {
                heatedAreaNumericUpDown.Value = value;
            }
        }

        /// <summary>
        /// Количество комнат
        /// </summary>
        public int RoomsCount
        {
            set
            {
                roomsCountNumericUpDown.Value = value;
            }
            get
            {
                return (int)roomsCountNumericUpDown.Value;
            }
        }

        /// <summary>
        /// В собственности
        /// </summary>
        public bool IsPrivate
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, IsPrivateCheckEdit);
            }
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(IsPrivateCheckEdit);
            }
        }

        /// <summary>
        /// Наличие лифта
        /// </summary>
        public bool LiftPresence
        {
            get
            {
                return liftPresenceCheckBox.Checked;
            }
            set
            {
                liftPresenceCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// Наличие мусоропровода
        /// </summary>
        public bool RubbishChutePresence
        {
            get
            {
                return rubbishChuteCheckBox.Checked;
            }
            set
            {
                rubbishChuteCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// Доступен ли контрол "Наличие лифта"
        /// </summary>
        public bool LiftPresenceEnabled
        {
            set
            {
                liftPresenceCheckBox.Enabled = value;
            }
        }

        /// <summary>
        /// Доступен ли контрол "Наличие мусоропровода"
        /// </summary>
        public bool RubbishChutePresenceEnabled
        {
            set
            {
                rubbishChuteCheckBox.Enabled = value;
            }
        }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Comment
        {
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, CommentTextBox);
            }
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(CommentTextBox);
            }
        }

        public bool WebAccess
        {
            get
            {
                return webAccessCheckBox.Checked;
            }
            set
            {
                webAccessCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// Email для доступа на веб сайт
        /// </summary>
        public string Email
        {
            get
            {
                return GetSimpleItemViewMapper.ViewToDomain(emailTextBox);
            }
            set
            {
                GetSimpleItemViewMapper.DomainToView(value, emailTextBox);
            }
        }

        /// <summary>
        /// Флаг реструктуризации долга
        /// </summary>
        public bool DebtsRepayment
        {
            get
            {
                return debtsRepaymentChkBox.Checked;
            }
            set
            {
                debtsRepaymentChkBox.Checked = value;
            }
        }

        /// <summary>
        /// Обновляет данные о физ. лице
        /// </summary>
        /// <param name="surname">Фамилия</param>
        /// <param name="firstName">Имя</param>
        /// <param name="patronymic">Отчество</param>
        public void UpdatePhysicalPerson(string surname, string firstName, string patronymic)
        {
            PhysicalPersonFullNameTextEdit.Text = string.Format("{0} {1} {2}", surname, firstName, patronymic);
            PhysicalPersonShortNameTextEdit.Text =
                string.Format(
                    "{0} {1} {2}",
                    surname,
                    string.IsNullOrEmpty(firstName)
                        ? string.Empty
                        : firstName.Substring(0, 1) + ".",
                    string.IsNullOrEmpty(patronymic)
                        ? string.Empty
                        : patronymic.Substring(0, 1) + ".");
        }

        private void UnknownRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (UnknownRadioButton.Checked)
            {
                Presenter.UpdateResidentsListView(true);
            }
        }

        /// <summary>
        /// Обрабатывает выбор типа собственника "Физическое лицо"
        /// </summary>
        private void PhysicalPersonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            PhysicalPersonFullNameTextEdit.Enabled = PhysicalPersonRadioButton.Checked;
            PhysicalPersonShortNameTextEdit.Enabled = PhysicalPersonRadioButton.Checked;
            if (PhysicalPersonRadioButton.Checked)
            {
                Presenter.UpdateResidentsListView(false);
            }
            else
            {
                PhysicalPersonFullNameTextEdit.Text = string.Empty;
                PhysicalPersonShortNameTextEdit.Text = string.Empty;
            }
        }

        /// <summary>
        /// Обрабатывает выбор типа собственника "Юридическое лицо"
        /// </summary>
        private void JuridicalPersonRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            JuridicalPersonFullNameTextEdit.Enabled = JuridicalPersonRadioButton.Checked;
            if (JuridicalPersonRadioButton.Checked)
            {
                Presenter.UpdateResidentsListView(true);
            }
        }

        /// <summary>
        /// Обработчик события изменения списка улиц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void streetLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (streetLookUpEdit.ItemIndex != -1)
            {
                Presenter.FillBuildingList();
            }
        }

        private void buildingLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (buildingLookUpEdit.ItemIndex != -1)
            {
                Presenter.SetFloorMax();
                Presenter.SetEntranceMax();
            }
        }

        private void PhysicalPersonFullNameTextEdit_Enter(object sender, EventArgs e)
        {
            Presenter.OnBeginPhysicalNameEdit(PhysicalPersonFullName);
        }

        private void PhysicalPersonFullNameTextEdit_Leave(object sender, EventArgs e)
        {
            Presenter.OnEndPhysicalNameEdit(PhysicalPersonFullName);
        }

        private void PhysicalPersonShortNameTextEdit_Enter(object sender, EventArgs e)
        {
            Presenter.OnBeginPhysicalNameEdit(PhysicalPersonShortName);
        }

        private void PhysicalPersonShortNameTextEdit_Leave(object sender, EventArgs e)
        {
            Presenter.OnEndPhysicalNameEdit(PhysicalPersonShortName);
        }

        private void pdLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Presenter.PrintStatement();
        }

        private void webAccessCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            emailTextBox.Enabled = webAccessCheckBox.Checked;

            if (!webAccessCheckBox.Checked)
            {
                emailTextBox.Text = null;
            }
        }

        private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            Presenter.PrintDebtRepaymentAgreement();
        }
    }
}