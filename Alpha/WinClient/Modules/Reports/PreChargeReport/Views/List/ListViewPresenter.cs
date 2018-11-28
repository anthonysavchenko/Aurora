using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.BusinessEntities.RefBook;
using Taumis.Alpha.Infrastructure.Interface.DataMappers.RefBook;
using Taumis.Alpha.Infrastructure.Library.Queries;
using Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Queries;
using Taumis.EnterpriseLibrary.Win.BaseViews.ReportView;

namespace Taumis.Alpha.WinClient.Aurora.Modules.Reports.PreChargeReport.Views.List
{
    public class ListViewPresenter : BaseReportForGridPresenter<IListView, EmptyReportParams>
    {
        private class PeriodValue
        {
            public decimal Charges { get; set; }
            public decimal Recharges { get; set; }
            public decimal Benefits { get; set; }
        }

        /// <summary>
        /// Выполняет действия при загрузке вида
        /// </summary>
        public override void OnViewReady()
        {
            View.Streets = GetList<Street>();
        }

        /// <summary>
        /// Возвращает данные для табличной части отчета
        /// </summary>
        /// <param name="_params">Параметры отчета</param>
        /// <returns>Данные табличной части отчета</returns>
        protected override DataTable GetGridData(EmptyReportParams _params)
        {
            DateTime _period = ServerTime.GetPeriodInfo().FirstUncharged;
            DateTime _prevPeriod = _period.AddMonths(-1);

            DataTable _table = new DataTable();
            _table.Columns.Add("Street");
            _table.Columns.Add("Building");
            _table.Columns.Add("Apartment");
            _table.Columns.Add("Account");
            _table.Columns.Add("NormCharge");
            _table.Columns.Add("CounterCharge");
            _table.Columns.Add("Diff");

            DataSet _ds = new DataSet { EnforceConstraints = false };
            _ds.Tables.Add(_table);

            int.TryParse(View.StreetId, out int _streetId);
            int.TryParse(View.BuildingId, out int _buildingId);

            using (var _db = new Entities())
            {
                Dictionary<int, decimal> _buildingVolumes =
                    (_buildingId > 0
                        ? _db.ElectricitySharedCounterVolumes.Where(x => x.BuildingID == _buildingId)
                        : _streetId > 0
                            ? _db.ElectricitySharedCounterVolumes.Where(x => x.Buildings.Streets.ID == _streetId)
                            : _db.ElectricitySharedCounterVolumes)
                    .Where(x => x.Period == _period)
                    .ToDictionary(x => x.BuildingID, x => x.Volume);

                Dictionary<int, Dictionary<int, decimal>> _ppAreaByBuilding = _db.GetPublicPlaceAreaByBuilding(_buildingId, _streetId);
                Dictionary<int, decimal> _buildingAreas = _db.GetBuildingArea(_period, _buildingId, _streetId);

                List<PublicPlaceElectricityServiceByBuildingAndCustomerQuery.Building> _data = 
                    _db.GetPublicPlaceElectricityServiceByBuildingAndCustomer(_period, _buildingId, _streetId);

                Dictionary<int, decimal> _normVolByBuilding = 
                    _db.GetPublicPlaceElectricityNormVolumeByBuilding(_period, _buildingAreas, _ppAreaByBuilding, _buildingId, _streetId);

                Dictionary<int, decimal> _counterVolByBuilding = 
                    _db.GetPublicPlaceElectricityCounterVolumeByBuilding(_period, _prevPeriod, _buildingId, _streetId);

                foreach (var _byBuild in _data)
                {
                    decimal _sharedCounterVol = _buildingVolumes.ContainsKey(_byBuild.BuildingID)
                        ? _buildingVolumes[_byBuild.BuildingID]
                        : 0;

                    decimal _buildingNormVol = _normVolByBuilding.ContainsKey(_byBuild.BuildingID)
                        ? _normVolByBuilding[_byBuild.BuildingID]
                        : 0;

                    decimal _buildingPrivateCounterVol = _counterVolByBuilding.ContainsKey(_byBuild.BuildingID)
                        ? _counterVolByBuilding[_byBuild.BuildingID]
                        : 0;

                    decimal _buildingArea = _buildingAreas.ContainsKey(_byBuild.BuildingID)
                        ? _buildingAreas[_byBuild.BuildingID]
                        : 0;

                    foreach (var _cust in _byBuild.Services)
                    {
                        decimal _ppArea =
                            _ppAreaByBuilding.ContainsKey(_byBuild.BuildingID) && _ppAreaByBuilding[_byBuild.BuildingID].ContainsKey(_cust.ServiceID)
                                ? _ppAreaByBuilding[_byBuild.BuildingID][_cust.ServiceID]
                                : 0;

                        decimal _normCharge = _cust.Norm * _ppArea * _cust.Square / _buildingArea * _cust.Rate;

                        decimal _counterCharge = _buildingArea > 0 && _sharedCounterVol > 0 
                            ? (_sharedCounterVol - _buildingPrivateCounterVol - _buildingNormVol) * _cust.Square / _buildingArea * _cust.Rate
                            : 0;

                        _normCharge = Math.Round(_normCharge, 2, MidpointRounding.AwayFromZero);
                        _counterCharge = Math.Round(_counterCharge, 2, MidpointRounding.AwayFromZero);

                        _table.Rows.Add(
                            _byBuild.Street,
                            _byBuild.BuildingNum,
                            _cust.Apartment,
                            _cust.Account,
                            _normCharge,
                            _counterCharge,
                            _normCharge - _counterCharge);
                    }
                }
            }

            return _table;
        }

        /// <summary>
        /// Заполняет список домов
        /// </summary>
        public void FillBuildingList()
        {
            View.Buildings = DataMapper<Building, IBuildingDataMapper>().GetBuildingsOnStreet(GetItem<Street>(View.StreetId));
        }
    }
}