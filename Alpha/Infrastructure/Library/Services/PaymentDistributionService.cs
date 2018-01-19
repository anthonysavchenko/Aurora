using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.CompositeUI;
using Taumis.Alpha.DataBase;
using Taumis.Alpha.Infrastructure.Interface.Services;

namespace Taumis.Alpha.Infrastructure.Library.Services
{
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
        public PeriodBalances DistributePayment(PeriodBalances periodBalances, DateTime paymentDate, DateTime paymentPeriod, DateTime lastChargedPeriod, decimal paymentValue, int customerId)
        {
            PeriodBalances _result = new PeriodBalances();

            Distribute(
                _result,
                periodBalances,
                paymentPeriod,
                paymentValue,
                lastChargedPeriod);

            return _result;
        }

        /// <summary>
        /// Распределяет переплату по периодам
        /// </summary>
        /// <param name="periodBalances">Данные о балансах по периодам</param>
        /// <param name="overpaymentValue">Сумма переплаты для распределения</param>
        /// <param name="distributionPeriod">Период распределения переплаты</param>
        /// <returns>Распределение переплаты по периодам и услугам</returns>
        public PeriodBalances DistributeOverpayment(PeriodBalances periodBalances, decimal overpaymentValue, DateTime distributionPeriod)
        {
            PeriodBalances _result = new PeriodBalances();

            Distribute(
                _result,
                periodBalances,
                distributionPeriod,
                overpaymentValue,
                distributionPeriod);

            return _result;
        }

        #region Функции распределения

        /// <summary>
        /// Распределяет значение по периодам и услугам
        /// </summary>
        /// <param name="result">Результат распределения</param>
        /// <param name="periodBalances">Данные по периодам</param>
        /// <param name="paymentPeriod">Оплачеваемый период</param>
        /// <param name="paymentValue">Сумма платежа</param>
        /// <param name="lastChargedPeriod">Последний начисленный период</param>
        private void Distribute(
            PeriodBalances result,
            PeriodBalances periodBalances,
            DateTime paymentPeriod,
            decimal paymentValue,
            DateTime lastChargedPeriod)
        {
            paymentValue = Math.Abs(paymentValue);

            List<DateTime> _debtPeriods =
                periodBalances.Balances
                    .Where(
                        b =>
                        b.Value.TotalBalance.Total > 0 &&
                        b.Key != paymentPeriod &&
                        b.Key <= lastChargedPeriod)
                    .Select(b => b.Key)
                    .OrderBy(p => p)
                    .ToList();

            if (periodBalances.Balances.ContainsKey(paymentPeriod) && periodBalances.Balances[paymentPeriod].TotalBalance.Total > 0)
            {
                _debtPeriods.Insert(0, paymentPeriod);
            }

            foreach (DateTime _period in _debtPeriods)
            {
                result.Balances.Add(_period, new ServiceBalances());

                if (paymentValue > 0)
                {
                    ServiceBalances _serviceBalances = periodBalances.Balances[_period];
                    decimal _valueToDistribute =
                        paymentValue >= _serviceBalances.TotalBalance.Total
                            ? _serviceBalances.TotalBalance.Total
                            : paymentValue;

                    Distribute(_valueToDistribute, _serviceBalances, result.Balances[_period], false);

                    paymentValue = paymentValue - _valueToDistribute;
                }
            }

            if (paymentValue > 0)
            {
                if (!result.Balances.ContainsKey(lastChargedPeriod))
                {
                    result.Balances.Add(lastChargedPeriod, new ServiceBalances());
                }

                ServiceBalances _lastServiceBalance = periodBalances.Balances.Values.LastOrDefault(b => b.TotalBalance.Charge > 0);

                if (_lastServiceBalance == null)
                {
                    _lastServiceBalance = periodBalances.Balances.Values.LastOrDefault(b => b.TotalBalance.Correction > 0);
                }

                Distribute(paymentValue, _lastServiceBalance, result.Balances[lastChargedPeriod], true);
            }
        }

        /// <summary>
        /// Распределяет сумму по услугам, использую переданный коэффициент
        /// </summary>
        /// <param name="valueToDistribute">Сумма для распределения</param>
        /// <param name="serviceBalances">Данные о балансах по услугам</param>
        /// <param name="resultDistribution">Результат распределения</param>
        /// <param name="distributeByCharge">Флаг распределения только по начислениям</param>
        private void Distribute(
            decimal valueToDistribute,
            ServiceBalances serviceBalances,
            ServiceBalances resultDistribution,
            bool distributeByCharge)
        {
            decimal _total = 0;
            decimal distributeValue = distributeByCharge
                ? serviceBalances.TotalBalance.Charge > 0
                    ? serviceBalances.TotalBalance.Charge
                    : serviceBalances.TotalBalance.Correction
                : serviceBalances.TotalBalance.Total;

            if(distributeValue <= 0)
            {
                throw new ApplicationException($"Не удалось распредилить платеж. Общая сумма, по которой необходимо распределить платеж <= 0: {distributeValue}");
            }

            decimal _coefficient = valueToDistribute / Math.Abs(distributeValue);
            decimal _valueSum = 0;

            foreach (KeyValuePair<int, Balance> _serviceBalance in serviceBalances.Balances)
            {
                decimal _balanceValue = distributeByCharge 
                    ? _serviceBalance.Value.Charge > 0
                        ? _serviceBalance.Value.Charge
                        : _serviceBalance.Value.Correction
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
                        resultDistribution.AddPayment(_serviceBalance.Key, -_value);
                        _valueSum += _value;
                    }
                }
            }

            if (valueToDistribute > _valueSum)
            {
                int _serviceID =
                    serviceBalances.Balances
                        .OrderByDescending(balance => Math.Abs(balance.Value.Total))
                        .First().Key;

                resultDistribution.AddPayment(_serviceID, -(valueToDistribute - _valueSum));
            }
        }

        #endregion
    }
}