using System;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.WinClient.Aurora.Library;
using Taumis.EnterpriseLibrary.Win;

namespace Taumis.Alpha.WinClient.Aurora.Modules.RefBooks.Buildings.Services
{
    public class BuildingUnitOfWork : UnitOfWork
    {
        /// <summary>
        /// Добавление новых элементов имеет особый порядок.
        /// </summary>
        protected override bool insertNew()
        {
            Type _counterType = typeof(CommonCounter);
            Type _counterValueType = typeof(CommonCounterValue);
            Type _publicPlaceType = typeof(PublicPlace);

            IDataMapper _counterDataMapper = DatMapServ.get(_counterType);
            IDataMapper _counterValueDataMapper = DatMapServ.get(_counterValueType);
            IDataMapper _publicPlaceDataMapper = DatMapServ.get(_publicPlaceType);

            bool _result = true;

            foreach (DomainObject _obj in newObjects.Values)
            {
                Type _type = _obj.GetType();
                if (_type == _counterType)
                {
                    _result = _counterDataMapper.update(_obj);
                    if (!_result)
                    {
                        break;
                    }
                }
                else if (_type == _publicPlaceType)
                {
                    _result = _publicPlaceDataMapper.update(_obj);
                    if (!_result)
                    {
                        break;
                    }
                }
            }

            foreach (DomainObject _obj in newObjects.Values)
            {
                if (_obj.GetType() == _counterValueType)
                {
                    _result = _counterValueDataMapper.update(_obj);
                    if (!_result)
                    {
                        break;
                    }
                }
            }

            return _result;
        }
    }
}