using System;
using System.Collections.Generic;
using System.Linq;

namespace Taumis.Alpha.Infrastructure.Interface.Services
{
    /*public class PeriodBalances
    {
        public IDictionary<DateTime, ServiceBalances> Balances
        {
            private set;
            get;
        }

        public PeriodBalances()
        {
            Balances = new Dictionary<DateTime, ServiceBalances>();
        }

        public PeriodBalances(Dictionary<DateTime, ServiceBalances> balances)
        {
            Balances = balances;
        }

        public void AddCharge(DateTime period, int key, decimal value)
        {
            if (!Balances.ContainsKey(period))
            {
                Balances.Add(period, new ServiceBalances());
            }

            Balances[period].AddCharge(key, value);
        }

        public void AddBenefit(DateTime period, int key, decimal value)
        {
            if (!Balances.ContainsKey(period))
            {
                Balances.Add(period, new ServiceBalances());
            }

            Balances[period].AddBenefit(key, value);
        }

        public void AddCorrection(DateTime period, int key, decimal value)
        {
            if (!Balances.ContainsKey(period))
            {
                Balances.Add(period, new ServiceBalances());
            }

            Balances[period].AddCorrection(key, value);
        }

        public void AddPayment(DateTime period, int key, decimal value)
        {
            if (!Balances.ContainsKey(period))
            {
                Balances.Add(period, new ServiceBalances());
            }

            Balances[period].AddPayment(key, value);
        }

        public void AddOverpayment(DateTime period, int key, decimal value)
        {
            if (!Balances.ContainsKey(period))
            {
                Balances.Add(period, new ServiceBalances());
            }

            Balances[period].AddOverpayment(key, value);
        }

        public void AddOverpaymentCorrection(DateTime period, int key, decimal value)
        {
            if (!Balances.ContainsKey(period))
            {
                Balances.Add(period, new ServiceBalances());
            }

            Balances[period].AddOverpaymentCorrection(key, value);
        }
    }

    public class ServiceBalances
    {
        public Balance TotalBalance
        {
            private set;
            get;
        }

        public IDictionary<int, Balance> Balances
        {
            private set;
            get;
        }

        public ServiceBalances()
        {
            Balances = new Dictionary<int, Balance>();
            TotalBalance = new Balance();
        }

        public ServiceBalances(IDictionary<int, Balance> balances)
        {
            Balances = balances;
            TotalBalance = new Balance();
            TotalBalance.AddCharge(balances.Values.Sum(balance => balance.Charge));
            TotalBalance.AddBenefit(balances.Values.Sum(balance => balance.Benefit));
            TotalBalance.AddCorrection(balances.Values.Sum(balance => balance.Correction));
            TotalBalance.AddPayment(balances.Values.Sum(balance => balance.Payment));
            TotalBalance.AddOverpayment(balances.Values.Sum(balance => balance.Overpayment));
            TotalBalance.AddOverpaymentCorrection(balances.Values.Sum(balance => balance.OverpaymentCorrection));
        }

        public ServiceBalances(IDictionary<int, Balance> balances, Balance totalBalance)
        {
            Balances = balances;
            TotalBalance = totalBalance;
        }

        public void AddCharge(int key, decimal value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].AddCharge(value);
            TotalBalance.AddCharge(value);
        }

        public void AddBenefit(int key, decimal value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].AddBenefit(value);
            TotalBalance.AddBenefit(value);
        }

        public void AddCorrection(int key, decimal value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].AddCorrection(value);
            TotalBalance.AddCorrection(value);
        }

        public void AddPayment(int key, decimal value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].AddPayment(value);
            TotalBalance.AddPayment(value);
        }

        public void AddOverpayment(int key, decimal value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].AddOverpayment(value);
            TotalBalance.AddOverpayment(value);
        }

        public void AddOverpaymentCorrection(int key, decimal value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].AddOverpaymentCorrection(value);
            TotalBalance.AddOverpaymentCorrection(value);
        }

        public void Add(int key, Balance value)
        {
            if (!Balances.ContainsKey(key))
            {
                Balances.Add(key, new Balance());
            }

            Balances[key].Add(value);
            TotalBalance.Add(value);
        }
    }

    public class Balance
    {
        public decimal Charge
        {
            private set;
            get;
        }

        public decimal Benefit
        {
            private set;
            get;
        }

        public decimal Correction
        {
            private set;
            get;
        }

        public decimal Payment
        {
            private set;
            get;
        }

        public decimal Overpayment
        {
            private set;
            get;
        }

        public decimal OverpaymentCorrection
        {
            private set;
            get;
        }

        public decimal Total
        {
            private set;
            get;
        }

        public Balance()
        {
        }

        public Balance(decimal charge, decimal benefit, decimal correction, decimal payment, decimal overpayment, decimal overpaymentCorrection, decimal total)
        {
            Charge = charge;
            Benefit = benefit;
            Correction = correction;
            Payment = payment;
            Overpayment = overpayment;
            OverpaymentCorrection = overpaymentCorrection;
            Total = total;
        }

        public void Add(Balance value)
        {
            Charge += value.Charge;
            Benefit += value.Benefit;
            Correction += value.Correction;
            Payment += value.Payment;
            Overpayment += value.Overpayment;
            OverpaymentCorrection += value.OverpaymentCorrection;
            Total += value.Total;
        }

        public void AddCharge(decimal value)
        {
            Charge += value;
            Total += value;
        }

        public void AddBenefit(decimal value)
        {
            Benefit += value;
            Total += value;
        }

        public void AddCorrection(decimal value)
        {
            Correction += value;
            Total += value;
        }

        public void AddPayment(decimal value)
        {
            Payment += value;
            Total += value;
        }

        public void AddOverpayment(decimal value)
        {
            Overpayment += value;
            Total += value;
        }

        public void AddOverpaymentCorrection(decimal value)
        {
            OverpaymentCorrection += value;
            Total += value;
        }
    }*/
}
