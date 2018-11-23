using System.Collections.Generic;
using Taumis.Alpha.Infrastructure.Interface.Common;

namespace Taumis.Alpha.Infrastructure.Interface.ExtensionMethods
{
    public static class BalanceDictionaryExtension
    {
        public static void AddCharge<TKey>(this Dictionary<TKey, Balance> dict, TKey key, decimal value)
        {
            AddIfNotContains(key, dict);
            dict[key].Charge += value;
        }

        public static void AddBenefit<TKey>(this Dictionary<TKey, Balance> dict, TKey key, decimal value)
        {
            AddIfNotContains(key, dict);
            dict[key].Benefit += value;
        }

        public static void AddOverpayment<TKey>(this Dictionary<TKey, Balance> dict, TKey key, decimal value)
        {
            AddIfNotContains(key, dict);
            dict[key].Overpayment += value;
        }

        public static Balance GetTotal<TKey>(this Dictionary<TKey, Balance> dict)
        {
            Balance _total = new Balance();

            foreach (Balance _b in dict.Values)
            {
                _total.Charge += _b.Charge;
                _total.Benefit += _b.Benefit;
                _total.Overpayment += _b.Overpayment;
                _total.Payment += _b.Payment;
                _total.Recharge += _b.Recharge;
            }

            return _total;
        }

        public static void AddCharges<TKey>(this Dictionary<TKey, Balance> dict, Dictionary<TKey, decimal> chargeDict)
        {
            foreach (var _pair in chargeDict)
            {
                if (!dict.ContainsKey(_pair.Key))
                {
                    dict.Add(_pair.Key, new Balance());
                }

                dict[_pair.Key].Charge += _pair.Value;
            }
        }

        public static void AddBenefits<TKey>(this Dictionary<TKey, Balance> dict, Dictionary<TKey, decimal> benefitDict)
        {
            foreach (var _pair in benefitDict)
            {
                if (!dict.ContainsKey(_pair.Key))
                {
                    dict.Add(_pair.Key, new Balance());
                }

                dict[_pair.Key].Benefit += _pair.Value;
            }
        }

        private static void AddIfNotContains<TKey>(TKey key, Dictionary<TKey, Balance> dict)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, new Balance());
            }
        }
    }
}
