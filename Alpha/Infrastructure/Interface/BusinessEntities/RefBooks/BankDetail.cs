using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class BankDetail : DomainObject
    {
        private string _name;

        /// <summary>
        /// Наименование банка
        /// </summary>
        public string Name
        {
            get
            {
                Load();
                return _name;
            }
            set
            {
                Load();
                _name = value;
            }
        }

        private string _bik;

        /// <summary>
        /// БИК
        /// </summary>
        public string BIK
        {
            get
            {
                Load();
                return _bik;
            }
            set
            {
                Load();
                _bik = value;
            }
        }

        private string _kpp;

        /// <summary>
        /// КПП
        /// </summary>
        public string KPP
        {
            get
            {
                Load();
                return _kpp;
            }
            set
            {
                Load();
                _kpp = value;
            }
        }

        private string _corrAccount;

        /// <summary>
        /// Корреспондентский счет
        /// </summary>
        public string CorrAccount
        {
            get
            {
                Load();
                return _corrAccount;
            }
            set
            {
                Load();
                _corrAccount = value;
            }
        }

        private string _account;

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public string Account
        {
            get
            {
                Load();
                return _account;
            }
            set
            {
                Load();
                _account = value;
            }
        }

        private string _inn;

        /// <summary>
        /// ИНН
        /// </summary>
        public string INN
        {
            get
            {
                Load();
                return _inn;
            }
            set
            {
                Load();
                _inn = value;
            }
        }
    }
}