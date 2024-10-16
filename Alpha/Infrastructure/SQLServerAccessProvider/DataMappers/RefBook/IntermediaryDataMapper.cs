﻿using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.EnterpriseLibrary.Infrastructure.SQLServerAccessProvider;
using Taumis.EnterpriseLibrary.Win;
using DBItem = Taumis.Alpha.DataBase.Intermediaries;
using DomItem = Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook.Intermediary;

namespace Taumis.Alpha.Infrastructure.SQLAccessProvider.DataMappers.RefBook
{
    /// <summary>
    /// Преобразователь данных для лицевого счета
    /// </summary>
    public class IntermediaryDataMapper : BaseDataMapper<DomItem, DBItem>
    {
        /// <summary>
        /// Преобразовывает объект прокси БД в объект домена
        /// </summary>
        /// <param name="obj">Объект домена</param>
        /// <returns>Объект домена</returns>
        protected override DomItem ServiceToBusiness(IDomainObject obj)
        {
            DomItem _domItem = (DomItem)obj;
            int _id = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                Intermediaries _dbItem = _entities.Intermediaries.First(x => x.ID == _id);

                _domItem.Name = _dbItem.Name;
                _domItem.Code = _dbItem.Code;
                _domItem.Rate = _dbItem.Rate;
            }

            return _domItem;
        }

        /// <summary>
        /// Преобразовавает объект домена в объект прокси БД
        /// </summary>
        /// <param name="domObj">Объект домена</param>
        /// <returns>Объект БД</returns>
        protected override DBItem BusinessToService(DomItem domObj)
        {
            DBItem _dbItem;

            using (Entities _entities = new Entities())
            {
                if (domObj.IsNew)
                {
                    _dbItem = new DBItem();
                    _entities.AddToIntermediaries(_dbItem);
                }
                else
                {
                    int _id = int.Parse(domObj.ID);
                    _dbItem = _entities.Intermediaries.First(x => x.ID == _id);
                }

                _dbItem.Name = domObj.Name;
                _dbItem.Code = domObj.Code;
                _dbItem.Rate = domObj.Rate;

                _entities.SaveChanges();
                domObj.ID = _dbItem.ID.ToString();
            }

            return _dbItem;
        }

        public override bool checkExistance(IDomainObject obj)
        {
            bool _result;
            int _domainId = int.Parse(obj.ID);

            using (Entities _entities = new Entities())
            {
                _result = null != _entities.Intermediaries.FirstOrDefault(x => x.ID == _domainId);
            }

            return _result;
        }

        /// <summary>
        /// Загружает список всех посредников
        /// </summary>
        public override object doLoad()
        {
            DataTable _table = new DataTable();
            _table.Columns.Add("ID", typeof(string));
            _table.Columns.Add("Name", typeof(string));

            using (Entities _entities = new Entities())
            {
                foreach (var _intermediary in _entities.Intermediaries)
                {
                    _table.Rows.Add(_intermediary.ID.ToString(), _intermediary.Name);
                }
            }

            return _table;
        }
    }
}