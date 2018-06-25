using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.Infrastructure.Interface.Common;
using Taumis.Alpha.Infrastructure.Interface.Services;
using Taumis.Alpha.Infrastructure.Interface.ExtensionMethods;
using Taumis.Alpha.DataBase;

namespace Taumis.Alpha.Infrastructure.Library.Services
{
    /// <summary>
    /// Сервис распределения платежа
    /// </summary>
    public class PaymentDistributionService
    {
        [ServiceDependency]
        public ISettingsService SettingsService { get; set; }

        /// <summary>
        /// Распределяет платеж по периоду платежа и периодам с задолженностями
        /// </summary>
        /// <param name="periodBalances">Данные о балансах по периодам</param>
        /// <param name="paymentDate">Дата платежа</param>
        /// <param name="paymentPeriod">Период, за который выполнен платеж</param>
        /// <param name="lastChargedPeriod">Период, за который выполнен платеж</param>
        /// <param name="paymentValue">Сумма платежа</param>
        /// <param name="customerId">ID абонента</param>
        /// <returns>Распределение платежа по периодам и услугам</returns>
        public Dictionary<DateTime, Dictionary<int, decimal>> DistributePayment(
            Dictionary<DateTime, Dictionary<int, Balance>> debtBalances,
            DateTime paymentDate,
            DateTime paymentPeriod,
            DateTime lastChargedPeriod,
            decimal paymentValue,
            int customerId)
        {
            return Distribute(
                debtBalances,
                paymentPeriod,
                paymentValue,
                lastChargedPeriod);
        }

        /// <summary>
        /// Распределяет переплату по периодам
        /// </summary>
        /// <param name="periodBalances">Данные о балансах по периодам</param>
        /// <param name="overpaymentValue">Сумма переплаты для распределения</param>
        /// <param name="distributionPeriod">Период распределения переплаты</param>
        /// <returns>Распределение переплаты по периодам и услугам</returns>
        public Dictionary<DateTime, Dictionary<int, decimal>> DistributeOverpayment(
            Dictionary<int, Balance> chargeBalances,
            decimal overpaymentValue,
            DateTime distributionPeriod)
        {
            return Distribute(
                new Dictionary<DateTime, Dictionary<int, Balance>> { { distributionPeriod, chargeBalances } },
                distributionPeriod,
                overpaymentValue,
                distributionPeriod);
        }

        #region Функции распределения

        /// <summary>
        /// Распределяет значение по периодам и услугам
        /// </summary>
        /// <param name="debtBalances">Данные по периодам</param>
        /// <param name="paymentPeriod">Оплачеваемый период</param>
        /// <param name="paymentValue">Сумма платежа</param>
        /// <param name="lastChargedPeriod">Последний начисленный период</param>
        private Dictionary<DateTime, Dictionary<int, decimal>> Distribute(
            Dictionary<DateTime, Dictionary<int, Balance>> debtBalances,
            DateTime paymentPeriod,
            decimal paymentValue,
            DateTime lastChargedPeriod)
        {
            Dictionary<DateTime, Dictionary<int, decimal>> _result = new Dictionary<DateTime, Dictionary<int, decimal>>();

            paymentValue = Math.Abs(paymentValue);

            List<DateTime> _debtPeriods =
                debtBalances
                    .Where(b =>
                        b.Value.Sum(x => x.Value.Total) > 0 &&
                        b.Key != paymentPeriod &&
                        b.Key <= lastChargedPeriod)
                    .Select(b => b.Key)
                    .OrderBy(p => p)
                    .ToList();

            if (debtBalances.ContainsKey(paymentPeriod) && debtBalances[paymentPeriod].Values.Sum(x => x.Total) > 0)
            {
                _debtPeriods.Insert(0, paymentPeriod);
            }

            foreach (DateTime _period in _debtPeriods)
            {
                if (paymentValue > 0)
                {
                    Dictionary<int, Balance> _serviceBalances = debtBalances[_period];
                    Balance _totalBalance = _serviceBalances.GetTotal();

                    decimal _valueToDistribute = paymentValue >= _totalBalance.Total
                        ? _totalBalance.Total : paymentValue;

                    _result.Add(
                        _period,
                        Distribute(_valueToDistribute, _serviceBalances, distributeByCharge: false));

                    paymentValue = paymentValue - _valueToDistribute;
                }
            }

            if (paymentValue > 0)
            {
                Dictionary<int, Balance> _lastChargedPeriodBalance = debtBalances.Values.LastOrDefault(x => x.GetTotal().Charge > 0);

                if (_lastChargedPeriodBalance == null)
                {
                    _lastChargedPeriodBalance = debtBalances.Values.LastOrDefault(x => x.GetTotal().Recharge > 0);
                    if (_lastChargedPeriodBalance == null)
                    {
                        throw new ApplicationException("Невозвожно распределить платеж/переплату. Отсутствуют данные начисления/перерасчета.");
                    }
                }

                Dictionary<int, decimal> _distr = Distribute(paymentValue, _lastChargedPeriodBalance, distributeByCharge: true);

                if (!_result.ContainsKey(lastChargedPeriod))
                {
                    _result.Add(lastChargedPeriod, _distr);
                }
                else
                {
                    _result[lastChargedPeriod].Add(_distr);
                }
            }

            return _result;
        }

        /// <summary>
        /// Распределяет сумму по услугам, использую переданный коэффициент
        /// </summary>
        /// <param name="valueToDistribute">Сумма для распределения</param>
        /// <param name="serviceBalances">Данные о балансах по услугам</param>
        /// <param name="resultDistribution">Результат распределения</param>
        /// <param name="distributeByCharge">Флаг распределения только по начислениям</param>
        private Dictionary<int, decimal> Distribute(
            decimal valueToDistribute,
            Dictionary<int, Balance> serviceBalances,
            bool distributeByCharge)
        {
            Dictionary<int, decimal> _result = new Dictionary<int, decimal>();

            Balance _totalBalance = serviceBalances.GetTotal();

            decimal _total = 0;
            decimal distributeValue = distributeByCharge
                ? _totalBalance.Charge > 0
                    ? _totalBalance.Charge
                    : _totalBalance.Recharge
                : _totalBalance.Total;

            if (distributeValue <= 0)
            {
                throw new ApplicationException($"Не удалось распредилить платеж. Общая сумма, по которой необходимо распределить платеж <= 0: {distributeValue}");
            }

            decimal _coefficient = valueToDistribute / Math.Abs(distributeValue);
            decimal _valueSum = 0;

            foreach (KeyValuePair<int, Balance> _serviceBalance in serviceBalances)
            {
                decimal _balanceValue = distributeByCharge
                    ? _serviceBalance.Value.Charge > 0
                        ? _serviceBalance.Value.Charge
                        : _serviceBalance.Value.Recharge
                    : _serviceBalance.Value.Total;

                if (_balanceValue > 0)
                {
                    decimal _value = Math.Round(_coefficient * _balanceValue, 2, MidpointRounding.AwayFromZero);

                    _total += _value;

                    if (_total > valueToDistribute)
                    {
                        _value -= _total - valueToDistribute;
                        _total = valueToDistribute;
                    }

                    if (_value > 0)
                    {
                        if (!_result.ContainsKey(_serviceBalance.Key))
                        {
                            _result.Add(_serviceBalance.Key, 0);
                        }
                        // Платеж в системе - значение со знаком минус
                        _result[_serviceBalance.Key] -= _value;

                        _valueSum += _value;
                    }
                }
            }

            if (valueToDistribute > _valueSum)
            {
                int _serviceID =
                    serviceBalances
                        .OrderByDescending(balance => Math.Abs(balance.Value.Total))
                        .First().Key;

                if (!_result.ContainsKey(_serviceID))
                {
                    _result.Add(_serviceID, 0);
                }
                _result[_serviceID] -= (valueToDistribute - _valueSum);
            }

            return _result;
        }

        #endregion
    }
}