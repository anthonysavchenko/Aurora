using System;
using System.Collections.Generic;
using System.Text;

using Taumis.EnterpriseLibrary.Win;

namespace Taumis.EnterpriseLibrary.Infrastructure.Common.Services
{
    /// <summary>
    /// Интерфейс коллекции объектов.
    /// </summary>
    public interface IIdentityMap
    {
        void Add(object obj);
        /// <summary>
        /// Возвращает объект домена по глобальному ключу.
        /// </summary>
        /// <param name="id">id@type</param>
        /// <returns></returns>
        object Get(string id);
    }
    
    /// <summary>
    /// Коллекция объектов. (IdemtityMap, стр.216) Универсальная (generic).
    /// Предназначен для кэширования данных на памяти клиента. 
    /// Существенно уменьшает количество обращений к серверу базы данных, 
    /// гарантирует однократную загрузку для всех объектов домена.
    /// </summary>
    public class IdentityMap: IIdentityMap
    {
        /// <summary>
        /// Конструктор Коллекции объектов.
        /// </summary>
        public IdentityMap()
        {
		    items = new Dictionary<string, object>();
	    }
        private IDictionary<string, object> items;

        #region IIdentityMap Members

        public void Add(object obj)
        {
            items.Add((obj as DomainObject).ID+"@"+obj.GetType().ToString(), obj);
        }

        public object Get(string id)
        {
            object res = null;
            if (items.ContainsKey(id))
            {
                items.TryGetValue(id, out res);
            }
            return res;
        }

        #endregion
    }
}
