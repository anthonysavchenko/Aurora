using System;
using System.Collections.Generic;
using System.Text;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.EnterpriseLibrary.Infrastructure.Common.Services
{
    /// <summary>
    /// Интерфейс элемента контроля за изменениями.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Очистить от всех изменений.
        /// </summary>
        void Clear();
        
        /// <summary>
        /// Зарегистрировать измененный.
        /// </summary>
        /// <param name="obj"></param>
        string registerDirty(DomainObject obj);
        
        /// <summary>
        /// Зарегистрировать удаленный.
        /// </summary>
        /// <param name="obj"></param>
        string registerRemoved(DomainObject obj);
        
        /// <summary>
        /// Зарегистрировать не измененный (просто кэширование).
        /// </summary>
        /// <param name="obj"></param>
        string registerClean(DomainObject obj);
        
        /// <summary>
        /// Зарегистрировать новый.
        /// </summary>
        /// <param name="obj"></param>
        string registerNew(DomainObject obj);
        
        /// <summary>
        /// Запуск бизнес-транзакции по внесению изменений в БД. 
        /// </summary>
        bool commit();

        /// <summary>
        /// Возвращает объект домена, если он есть
        /// в кэше Unit Of Work.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DomainObject get(string id);
    }

    /// <summary>
    /// Интерфейс Единицы работы для тестирования.
    /// </summary>
    public interface ITestUnitOfWork : IUnitOfWork
    {
        IDictionary<string, DomainObject> New { get;}
        IDictionary<string, DomainObject> Dirty { get; }
        IDictionary<string, DomainObject> Removed { get; }
        IIdentityMap Clean { get; }
    }
}
