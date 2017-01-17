using System;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook
{
    public class User : DomainObject
    {
        private string _login;
        /// <summary>
        /// Логин
        /// </summary>
        public string Login
        {
            get
            {
                Load();
                return _login;
            }
            set
            {
                Load();
                _login = value;
            }
        }

        private string _password;
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password
        {
            get
            {
                Load();
                return _password;
            }
            set
            {
                Load();
                _password = value;
            }
        }

        private string _aka;
        /// <summary>
        /// ФИО
        /// </summary>
        public string Aka
        {
            get
            {
                Load();
                return _aka;
            }
            set
            {
                Load();
                _aka = value;
            }
        }

        private string _securityStamp;
        public string SecurityStamp
        {
            get
            {
                Load();
                return _securityStamp;
            }
            set
            {
                Load();
                _securityStamp = value;
            }
        }

        private DateTime? _lockoutEndDateUtc;
        public DateTime? LockoutEndDateUtc
        {
            get
            {
                Load();
                return _lockoutEndDateUtc;
            }
            set
            {
                Load();
                _lockoutEndDateUtc = value;
            }
        }

        private bool _lockoutEnabled;
        public bool LockoutEnabled
        {
            get
            {
                Load();
                return _lockoutEnabled;
            }
            set
            {
                Load();
                _lockoutEnabled = value;
            }
        }

        private int _accessFailedCount;
        public int AccessFailedCount
        {
            get
            {
                Load();
                return _accessFailedCount;
            }
            set
            {
                Load();
                _accessFailedCount = value;
            }
        }
    }
}