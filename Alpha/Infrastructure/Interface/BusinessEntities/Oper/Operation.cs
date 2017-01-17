using System;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Domain.Oper
{
    /// <summary>
    /// Базовый класс операции
    /// </summary>
    public abstract class Operation : DomainObject, Taumis.Domain.Common.ITransactionOwner, IArchival
    {
        private bool _archived;
        /// <summary>
        /// Признак архивированности операции
        /// </summary>
        public bool IsArchived
        {
            get
            {
                Load();
                return _archived;
            }
            set
            {
                Load();
                _archived = value;
            }
        }

        private bool _transacted;
        /// <summary>
        /// Признак утвержденности документа
        /// </summary>
        public bool IsTransacted
        {
            get
            {
                Load();
                return _transacted;
            }
            set
            {
                Load();
                _transacted = value;
            }
        }

        private string _operNumber;
        /// <summary>
        /// Номер операции
        /// </summary>
        public string OperNumber
        {
            get
            {
                Load();
                return _operNumber;
            }
            set
            {
                Load();
                _operNumber = value;
            }
        }

        private DateTime _docDateTime;
        /// <summary>
        /// ДатаВремя совершения операции
        /// </summary>
        public DateTime DocDateTime
        {
            get
            {
                Load();
                return this._docDateTime;
            }
            set
            {
                Load();
                this._docDateTime = value;
            }
        }
    }
}