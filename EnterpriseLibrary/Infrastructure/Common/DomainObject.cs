using System;
using System.Diagnostics;
using Taumis.EnterpriseLibrary.Win.Services;

namespace Taumis.EnterpriseLibrary.Win
{
    public static class DataMapServHolderForDomain
    {
        private static IDataMapperService _dataMapServ;
        public static IDataMapperService DataMapServ
        {
            get
            {
                return _dataMapServ;
            }
        }

        public static void SaveDataMapService(IDataMapperService dataMapServ)
        {
            _dataMapServ = dataMapServ;
        }
    }

    /// <summary>
    /// Интерфейс супертипа слоя домена.
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Свойство доступа к уникальному идентификатору
        /// </summary>
        string ID { get; set;}

        /// <summary>
        /// Устанавливает статус фиктивного объекта. 
        /// Должен вызываться только из Датамаппера при создании фиктивного объекта
        /// </summary>
        /// <param name="_id">Уникальный идентификатор</param>
        void SetGhostStatus(string _id);

        /// <summary>
        /// Устанавливает статус загруженного объекта после сохранения
        /// </summary>
        /// <remarks>Должен вызываться только из датамаппера после сохранения</remarks>
        void SetLoadedStatusAfterSave();

        /// <summary>
        /// Возвращает признак возможности сохранения объекта
        /// </summary>
        /// <returns>true если можно сохранять, иначе false</returns>
        bool IsAllowedToSave();
    }

    /// <summary>
    /// Супертип слоя модели домена. Layer Supertype, 491.
    /// </summary>
    public class DomainObject : IDomainObject
    {

        private string id = Guid.NewGuid().ToString();
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Хранит состояние загрузки объекта
        /// </summary>
        private LoadStatus _loadStatus = LoadStatus.NEW;

        /// <summary>
        /// Состояния загрузки объекта
        /// </summary>  
        private enum LoadStatus
        {
            /// <summary>
            /// Новый объект. Не имеет представления в БД
            /// </summary>
            NEW,

            /// <summary>
            /// Фиктивный объект. Имеет представление в БД, но не содержит данных
            /// </summary>            
            GHOST,

            /// <summary>
            /// В состоянии загрузки. Фиктивный объект наполняется реальными данными
            /// </summary>            
            LOADING,

            /// <summary>
            /// Загруженный объект
            /// </summary>            
            LOADED,

            /// <summary>
            /// Загруженный объект, который не имеет представления в БД
            /// </summary>            
            LOADED_MISSING
        };

        /// <summary>
        /// Устанавливает статус фиктивного объекта. 
        /// Должен вызываться только из Датамаппера при создании фиктивного объекта
        /// </summary>
        /// <param name="_id">Уникальный идентификатор</param>
        public void SetGhostStatus(string _id)
        {
            _loadStatus = LoadStatus.GHOST;
            ID = _id;
        }

        /// <summary>
        /// Устанавливает статус загруженного объекта после сохранения
        /// </summary>
        /// <remarks>Должен вызываться только из датамаппера после сохранения</remarks>
        public void SetLoadedStatusAfterSave()
        {
            if (_loadStatus == LoadStatus.NEW)
            {
                _loadStatus = LoadStatus.LOADED;
            }
        }

        /// <summary>
        /// Возвращает признак возможности сохранения объекта
        /// </summary>
        /// <returns>true если можно сохранять, иначе false</returns>
        public bool IsAllowedToSave()
        {
            // Сохранять можно только новые или полностью загруженные объекты
            return (_loadStatus == LoadStatus.LOADED || _loadStatus == LoadStatus.NEW);
        }

        /// <summary>
        /// Выполняет загрузку реального объекта из БД.
        /// Должен вызываться в каждом методе доступа каждого объекта домена
        /// </summary>
        protected void Load()
        {
            // Имеет смысл загружать только фиктивные объекты
            if (_loadStatus == LoadStatus.GHOST)
            {
                _loadStatus = LoadStatus.LOADING;

                // Статус загрузки устанавливаем в зависимости
                if (DataMapServHolderForDomain.DataMapServ.get(this.GetType()).findReal(this) == null)
                {
                    _loadStatus = LoadStatus.LOADED_MISSING;
                }
                else
                {
                    _loadStatus = LoadStatus.LOADED;
                }
            }
        }

        /// <summary>
        /// Проверяет существование представления объекта в БД
        /// </summary>
        private bool CheckExistance()
        {
            bool _res = false;

            // Имеет смысл проверять только фиктивные объекты
            if (_loadStatus == LoadStatus.GHOST)
            {
                // Статус загрузки устанавливаем в зависимости
                _res = DataMapServHolderForDomain.DataMapServ.get(this.GetType()).checkExistance(this);

                // Можно сразу сказать что этого объекта нет в БД
                if (!_res)
                {
                    _loadStatus = LoadStatus.LOADED_MISSING;
                }
            }

            return _res;
        }

        /// <summary>
        /// Клонирует объект
        /// </summary>
        /// <returns>Клонированный объект</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool IsNew
        {
            get
            {
                return _loadStatus == LoadStatus.NEW;
            }
        }

        #region Перегрузка операторов сравнения

        public override bool Equals(object obj)
        {
            bool _res = base.Equals(obj);

            //Сравнение с null требует отдельной проверки
            if (!_res && obj == null)
            {
                // Проверяем существует ли представление домена в БД
                CheckExistance();

                // Если представления домена нет в БД, значит он равен null
                _res = (this._loadStatus == LoadStatus.LOADED_MISSING);
            }

            return _res;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        [DebuggerHidden]
        public static bool operator ==(DomainObject _obj1, DomainObject _obj2)
        {
            bool _res = false;

            try
            {
                _res = _obj1.Equals(_obj2);
            }
            catch
            {
                try
                {
                    _res = _obj2.Equals(_obj1);
                }
                catch
                {
                    _res = true;
                }
            }

            return _res;
        }

        [DebuggerHidden]
        public static bool operator !=(DomainObject _obj1, DomainObject _obj2)
        {
            return !(_obj1 == _obj2);
        }

        #endregion
    }
}
