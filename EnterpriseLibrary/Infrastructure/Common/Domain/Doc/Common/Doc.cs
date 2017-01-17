using System;
using System.Collections.Generic;

using Taumis.Domain.DocLine;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Domain.Doc
{
    /// <summary>
    /// Базовый документ. Супертип слоя документов.
    /// </summary>
    public abstract class Doc : DomainObject, IDoc
    {
        /// <summary>
        /// Конструктор инициирует словарь строк.
        /// </summary>
        public Doc()
        {
            _lines = new Dictionary<string, IDocLine>();
        }

        #region IDoc Members

        private bool _archived;
        /// <summary>
        /// Признак архивированности документа
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

        private object _docAuthor;
        /// <summary>
        /// Автор документа - пользователь.
        /// </summary>
        public object Author
        {
            get
            {
                Load();
                return this._docAuthor;
            }
            set
            {
                Load();
                this._docAuthor = value;
            }
        }

        private object _docOwner;
        /// <summary>
        /// Контрагент - владелец документа.
        /// </summary>
        public object Owner
        {
            get
            {
                Load();
                return this._docOwner;
            }
            set
            {
                Load();
                this._docOwner = value;
            }
        }

        private Dictionary<string, IDocLine> _lines;
        /// <summary>
        /// Строки документа.
        /// </summary>        
        public Dictionary<string, IDocLine> Lines
        {
            get
            {
                Load();
                return this._lines;
            }
        }

        private string _docNum;
        /// <summary>
        /// Номер документа.
        /// </summary>
        public string DocNumber
        {
            get
            {
                Load();
                return this._docNum;
            }
            set
            {
                Load();
                this._docNum = value;
            }
        }

        private DateTime _docDateTime;
        /// <summary>
        /// ДатаВремя документа
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

        #endregion
    }
}