using Microsoft.Practices.CompositeUI;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Practices.CompositeUI.EventBroker;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.Doc;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Library.Services;
using Taumis.Alpha.WinClient.Aurora.Modules.Service.Import.Services;
using Taumis.EnterpriseLibrary.Win;
using Taumis.EnterpriseLibrary.Win.BaseViews.BaseLayoutView;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Service.Import
{
    public class CustomerPosValue
    {
        public Taumis.Alpha.DataBase.CustomerPoses CustomerPos
        {
            set;
            get;
        }

        public decimal Value
        {
            set;
            get;
        }
    }

    /// <summary>
    /// Презентер вью формы
    /// </summary>
    public class LayoutViewPresenter : BaseLayoutViewPresenter<ILayoutView>
    {
        private const string GISZHKH_PROCESSING_START = "event://Import/GisZhkh/Start";
        private const string GISZHKH_PROCESSING_COMPLETED = "event://Import/GisZhkh/Completed";

        private readonly IGisZhkhCustomersImportService _gisZhkhCustomersImportService = new GisZhkhCustomersImportService();

        /// <summary>
        /// Сервис работы с доменами, умеющими работать с датамаппером
        /// </summary>
        [ServiceDependency]
        public IDomainWithDataMapperHelperService DomainWithDataMapperHelperServ
        {
            set;
            private get;
        }

        /// <summary>
        /// Stores payment distibution service reference
        /// </summary>
        [ServiceDependency]
        public PaymentDistributionService PaymentDistributionSrv
        {
            set;
            private get;
        }

        /// <summary>
        /// Получить объект домена типа T по его ID
        /// </summary>
        /// <typeparam name="T">Тип объекта домена</typeparam>
        /// <param name="_id">ID объекта</param>
        /// <returns>Объект домента типа Т</returns>
        public T GetItem<T>(string _id)
            where T : DomainObject
        {
            return DomainWithDataMapperHelperServ.GetItem<T>(_id);
        }

        /// <summary>
        /// Обрабатывает активацию модуля
        /// </summary>
        public override void ActivateUseCase()
        {
            /* Не реализованно */
        }

        /// <summary>
        /// Открывает диалог открытия файла
        /// </summary>
        /// <returns></returns>
        public void FindFile()
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            _openFileDialog.InitialDirectory = Application.StartupPath + @"\Data";
            _openFileDialog.Title = "Открыть файл";
            _openFileDialog.Filter = "Книга Microsoft Excel 97-2003|*.xls|Книга Microsoft Excel 2007|*.xlsx";
            _openFileDialog.FilterIndex = 1;
            _openFileDialog.RestoreDirectory = true;
            _openFileDialog.DefaultExt = "xls";

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                View.FilePath = _openFileDialog.FileName;
            }
            else
            {
                View.FilePath = String.Empty;
            }
        }

        /// <summary>
        /// Открывает диалог открытия файла для импорта абонентов
        /// </summary>
        /// <returns></returns>
        public void ImportCustomersFindFile()
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();

            _openFileDialog.InitialDirectory = Application.StartupPath + @"\Data";
            _openFileDialog.Title = "Открыть файл";
            _openFileDialog.Filter = "Книга Microsoft Excel 97-2003|*.xls|Книга Microsoft Excel 2007|*.xlsx";
            _openFileDialog.FilterIndex = 1;
            _openFileDialog.RestoreDirectory = true;
            _openFileDialog.DefaultExt = "xls";

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                View.ImportCustomersFilePath = _openFileDialog.FileName;
            }
            else
            {
                View.ImportCustomersFilePath = String.Empty;
            }
        }

        /// <summary>
        /// Импортирует услуги
        /// </summary>
        public void ImportFile()
        {
            int _currentRow = 1;

            try
            {
                using (ExcelSheet _sheet = new ExcelSheet(View.FilePath, "Лист1"))
                {
                    while (_currentRow < _sheet.RowsCount)
                    {
                        _currentRow++;

                        string _street = FirstLettersToUpper(_sheet.GetCell("A", _currentRow));
                        string _building = _sheet.GetCell("B", _currentRow);
                        string _serviceTypeName = _sheet.GetCell("C", _currentRow);
                        string _serviceName = _sheet.GetCell("D", _currentRow);
                        string _contractorName = _sheet.GetCell("E", _currentRow);
                        decimal _rate = Convert.ToDecimal(_sheet.GetCell("F", _currentRow));
                        DateTime _since = DateTime.Parse(_sheet.GetCell("G", _currentRow));
                        DateTime _till = DateTime.Parse(_sheet.GetCell("H", _currentRow));

                        using (Entities _db = new Entities())
                        {
                            DataBase.Services _service = _db.Services.FirstOrDefault(service => service.Name == _serviceName);
                            ServiceTypes _serviceType = _db.ServiceTypes.FirstOrDefault(serviceType => serviceType.Name == _serviceTypeName);
                            Contractors _contractor = _db.Contractors.FirstOrDefault(contractor => contractor.Name == _contractorName);

                            if (_serviceType == null)
                            {
                                _serviceType = new ServiceTypes
                                {
                                    Name = _serviceTypeName,
                                    Code = _serviceTypeName
                                };
                            }

                            if (_service == null)
                            {
                                _service = new DataBase.Services
                                {
                                    Name = _serviceName,
                                    Code = _serviceName,
                                    ChargeRule = 1,
                                    ServiceTypes = _serviceType
                                };
                            }

                            if (_contractor == null)
                            {
                                _contractor = new Contractors
                                {
                                    Name = _contractorName,
                                    Code = _contractorName
                                };
                            }

                            IQueryable<Customers> _query =
                                from customer in _db.Customers
                                where customer.Buildings.Streets.Name == _street && customer.Buildings.Number == _building
                                select customer;

                            foreach (Customers _customer in _query)
                            {
                                _db.CustomerPoses.AddObject(
                                    new CustomerPoses
                                    {
                                        Services = _service,
                                        Rate = _rate,
                                        Contractors = _contractor,
                                        Customers = _customer,
                                        Since = _since,
                                        Till = _till
                                    });
                            }

                            _db.SaveChanges();
                        }
                    }
                }

                View.ShowMessage("Импорт успешно завершен", "Импорт");
            }
            catch (Exception _exception)
            {
                View.ShowMessage(_exception.Message, "Ошибка импорта");
            }
        }

        /// <summary>
        /// Импортирует абонентов
        /// </summary>
        public void ImportCustomersImport()
        {
            int _addedCustomersCount = 0;
            int _currentRow = 1;
            string _allreadyExistedAccounts = String.Empty;

            try
            {
                using (ExcelSheet sheet = new ExcelSheet(View.ImportCustomersFilePath, "Лист1"))
                {
                    while (_currentRow < sheet.RowsCount)
                    {
                        _currentRow++;

                        bool _accountExists = false;
                        string _account;
                        string _physicalPersonFullName = String.Empty;
                        string _physicalPersonShortName = String.Empty;
                        string _juridicalPersonFullName = String.Empty;
                        string _street = FirstLettersToUpper(sheet.GetCell("B", _currentRow)).Trim();
                        string _house = sheet.GetCell("C", _currentRow).Trim();
                        string _part = sheet.GetCell("D", _currentRow).Trim();
                        string _houseAndPart = !String.IsNullOrEmpty(_part) ? String.Format("{0}, Корпус {1}", _house, _part) : _house;
                        string _apartment = sheet.GetCell("E", _currentRow).Trim();
                        Customer.OwnerTypes _ownerType = Customer.OwnerTypes.Unknown;
                        int _constOwnerType = 0;

                        switch (sheet.GetCell("G", _currentRow))
                        {
                            case "Физическое лицо":
                                _ownerType = Customer.OwnerTypes.PhysicalPerson;
                                _constOwnerType = 1;
                                _physicalPersonFullName = FirstLettersToUpper(sheet.GetCell("F", _currentRow));
                                _physicalPersonShortName = GetLastNameAndInitial(_physicalPersonFullName);
                                break;
                            case "Юридическое лицо":
                                _ownerType = Customer.OwnerTypes.JuridicalPerson;
                                _constOwnerType = 2;
                                _juridicalPersonFullName = FirstLettersToUpper(sheet.GetCell("F", _currentRow));
                                break;
                        }

                        using (Entities _entities = new Entities())
                        {
                            if (_entities.Customers.FirstOrDefault(customer => ((customer.OwnerType == 0 && _constOwnerType == 0) ||
                                                                                (customer.OwnerType == 1 && _constOwnerType == 1 && customer.PhysicalPersonFullName == _physicalPersonFullName) ||
                                                                                (customer.OwnerType == 2 && _constOwnerType == 2 && customer.JuridicalPersonFullName == _juridicalPersonFullName)) &&
                                                                               customer.Buildings.Streets.Name == _street &&
                                                                               customer.Buildings.Number == _houseAndPart &&
                                                                               customer.Apartment == _apartment) != null)
                            {
                                _accountExists = true;
                            }
                        }

                        if (!_accountExists)
                        {
                            string _domStreetId;
                            string _domBuildingId;

                            using (Entities _entities = new Entities())
                            {
                                Taumis.Alpha.DataBase.Customers _lastCustomer = _entities.Customers.OrderByDescending(customer => customer.Account).FirstOrDefault();
                                if (_lastCustomer != null)
                                {
                                    long _lastAccount = Convert.ToInt64(String.Format("{0}{1}{2}", _lastCustomer.Account.Substring(3, 4), _lastCustomer.Account.Substring(8, 3), _lastCustomer.Account.Substring(12, 1)));
                                    _account = (_lastAccount + 1).ToString().Insert(7, "-").Insert(4, "-").Insert(0, "EG-");
                                }
                                else
                                {
                                    _account = "EG-1111-111-1";
                                }

                                var _dbStreet = _entities.Streets.FirstOrDefault(s => s.Name == _street);
                                _domStreetId = _dbStreet != null ? _dbStreet.ID.ToString() : null;

                                var _dbBuilding = _entities.Buildings.FirstOrDefault(b => b.Number == _houseAndPart && b.Streets.Name == _street);
                                _domBuildingId = _dbBuilding != null ? _dbBuilding.ID.ToString() : null;
                            }

                            Street _domStreet;
                            Building _domBuilding;

                            if (string.IsNullOrEmpty(_domStreetId))
                            {
                                _domStreet = new Street
                                {
                                    Name = _street
                                };
                                DomainWithDataMapperHelperServ.UpdateItem(_domStreet);
                            }
                            else
                            {
                                _domStreet = GetItem<Street>(_domStreetId);
                            }

                            if (string.IsNullOrEmpty(_domBuildingId))
                            {
                                _domBuilding = new Building
                                {
                                    Number = _houseAndPart,
                                    ZipCode = sheet.GetCell("L", _currentRow),
                                    Street = _domStreet
                                };
                                DomainWithDataMapperHelperServ.UpdateItem(_domBuilding);
                            }
                            else
                            {
                                _domBuilding = GetItem<Building>(_domBuildingId);
                            }


                            Customer _customer = new Customer()
                            {
                                Account = _account,
                                Apartment = _apartment,
                                Building = _domBuilding,
                                OwnerType = _ownerType,
                                PhysicalPersonShortName = _physicalPersonShortName,
                                PhysicalPersonFullName = _physicalPersonFullName,
                                JuridicalPersonFullName = _juridicalPersonFullName,
                                Square = Convert.ToDecimal(sheet.GetCell("H", _currentRow)),
                                IsPrivate = sheet.GetCell("I", _currentRow) == "*",
                                RoomsCount = Convert.ToInt32(sheet.GetCell("J", _currentRow)),
                            };
                            DomainWithDataMapperHelperServ.UpdateItem(_customer);
                            _addedCustomersCount++;
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(_allreadyExistedAccounts))
                            {
                                _allreadyExistedAccounts = String.Format("\r\n\r\nСледующие абоненты из файла уже существуют в базе данных, строки: {0}", _currentRow);
                            }
                            else
                            {
                                _allreadyExistedAccounts = String.Format("{0}, {1}", _allreadyExistedAccounts, _currentRow);
                            }
                        }
                    }

                    MessageBox.Show(String.Format("Добавлено абонентов: {0}{1}", _addedCustomersCount, _allreadyExistedAccounts), "Импорт успешно завершен");
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(String.Format("Добавлено абонентов: {0}{1}\r\n\r\n{2}", _addedCustomersCount, _allreadyExistedAccounts, ex.Message), "Ошибка импорта");
            }
            catch (TargetInvocationException ex)
            {
                MessageBox.Show(String.Format("Добавлено абонентов: {0}{1}\r\n\r\n{2}", _addedCustomersCount, _allreadyExistedAccounts, ex.Message), "Ошибка импорта");
            }
            catch (Exception ex)
            {
                string _rowNumberMessage = String.Empty;

                if (_currentRow > 1)
                {
                    _rowNumberMessage = String.Format("Ошибка произошла при обработке строки №{0}.\r\n\r\n", _currentRow);
                }

                MessageBox.Show(String.Format("{0}Добавлено абонентов: {1}{2}\r\n\r\n{3}", _rowNumberMessage, _addedCustomersCount, _allreadyExistedAccounts, ex.Message), "Критическая ошибка импорта");
            }
        }

        /// <summary>
        /// Добавляет услугу абонентам
        /// </summary>
        public void CreateServicesForCustomers()
        {
            using (Entities entities = new Entities())
            {
                int _serviceID;
                int _contractorID;
                Int32.TryParse(View.Service.ID, out _serviceID);
                Int32.TryParse(View.Contractor.ID, out _contractorID);
                IQueryable<Taumis.Alpha.DataBase.Customers> _query = entities.Customers;
                Taumis.Alpha.DataBase.Services _service = entities.Services.Where(service => service.ID == _serviceID).FirstOrDefault();
                Taumis.Alpha.DataBase.Contractors _contractor = entities.Contractors.Where(contractor => contractor.ID == _contractorID).FirstOrDefault();

                if (View.IsPrivate)
                {
                    _query = entities.Customers.Where(customer => customer.IsPrivate);
                }

                foreach (Taumis.Alpha.DataBase.Customers customer in _query)
                {
                    customer.CustomerPoses.Add(new CustomerPoses()
                    {
                        Services = _service,
                        Rate = View.Rate,
                        Contractors = _contractor,
                        Customers = customer
                    });
                }

                entities.SaveChanges();
            }
        }

        /// <summary>
        /// Обновить все списки комбобоксов
        /// </summary>
        internal void RefreshServices()
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities entities = new Entities())
            {
                foreach (Taumis.Alpha.DataBase.Services service in entities.Services)
                {
                    _table.Rows.Add(
                        service.ID,
                        service.Name);
                }
            }

            View.Services = _table;
        }

        /// <summary>
        /// Обновить все списки комбобоксов
        /// </summary>
        internal void RefreshContractors()
        {
            DataTable _table = new DataTable();

            _table.Columns.Add("ID", typeof(int));
            _table.Columns.Add("Name", typeof(string));

            using (Entities entities = new Entities())
            {
                foreach (Taumis.Alpha.DataBase.Contractors contaractor in entities.Contractors)
                {
                    _table.Rows.Add(
                        contaractor.ID,
                        contaractor.Name);
                }
            }

            View.Contractors = _table;
        }

        /// <summary>
        /// Обработка загрузки вью
        /// </summary>
        public override void OnViewReady()
        {
            base.OnViewReady();
            RefreshServices();
            RefreshContractors();
        }


        /// <summary>
        /// Переводит начальные буквы во всех словах строки в верхний регистр
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns>Строка с начальными буквами всех слов в верхнем регистре</returns>
        private string FirstLettersToUpper(string source)
        {
            string[] _words = source.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < _words.Length; i++)
            {
                string[] _subWords = _words[i].Split(new char[] { '-' }, StringSplitOptions.None);

                for (int j = 0; j < _subWords.Length; j++)
                {
                    _subWords[j] = String.Format("{0}{1}", Char.ToUpper(_subWords[j][0]), _subWords[j].Remove(0, 1));
                }

                _words[i] = String.Join("-", _subWords);
            }

            return String.Join(" ", _words);
        }

        /// <summary>
        /// Возвращает фамилии и инициалы
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns>Фамилия без изменений (если она есть в исходной строке) и инициалы (если они есть в исходной строке), разделенные пробелами, для каждого имени в строке, разделенные точкой с запятой</returns>
        private string GetLastNameAndInitial(string source)
        {
            string[] _names = source.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < _names.Length; i++)
            {
                string[] _words = _names[i].Split(new char[] { ' ' }, 3, StringSplitOptions.RemoveEmptyEntries);

                if (_words.Length > 2)
                {
                    _names[i] = String.Join(" ", new string[] { _words[0], String.Format("{0}.", _words[1][0]), String.Format("{0}.", _words[2][0]) });
                }
                else if (_words.Length > 1)
                {
                    _names[i] = String.Join(" ", new string[] { _words[0], String.Format("{0}.", _words[1][0]) });
                }
                else if (_words.Length > 0)
                {
                    _names[i] = _words[0];
                }
            }

            return String.Join(";", _names);
        }

        public void GisZhkhImport()
        {
            if (string.IsNullOrEmpty(View.GisZhkhInputFilePath))
            {
                View.ShowMessage("Укажите файл для импорта данных ГИС ЖКХ", "Ошибка импорта");
            }
            else
            {
                View.ShowGisZhkhProgressBar();
                GisZhkhProcessingStart(this, EventArgs.Empty);
            }
        }

        [EventPublication(GISZHKH_PROCESSING_START, PublicationScope.WorkItem)]
        public event EventHandler GisZhkhProcessingStart;

        [EventSubscription(GISZHKH_PROCESSING_START, ThreadOption.Background)]
        public void OnGisZhkhProcessingStart(object sender, EventArgs e)
        {
            string _message = _gisZhkhCustomersImportService.ProcessFile(View.GisZhkhInputFilePath);
            GisZhkhProcessingCompleted(this, new EventArgs<string>(_message));
        }

        [EventPublication(GISZHKH_PROCESSING_COMPLETED, PublicationScope.WorkItem)]
        public event EventHandler<EventArgs<string>> GisZhkhProcessingCompleted;

        [EventSubscription(GISZHKH_PROCESSING_COMPLETED, ThreadOption.UserInterface)]
        public void OnGisZhkhProcessingCompleted(object sender, EventArgs<string> e)
        {
            View.HideGisZhkhProgressBar();
            View.ShowMessage(e.Data, "Импорт");
        }
    }
}