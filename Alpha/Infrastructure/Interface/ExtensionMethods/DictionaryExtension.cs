using System.Collections.Generic;

namespace Taumis.Alpha.Infrastructure.Interface.ExtensionMethods
{
    public static class DictionaryExtension
    {
        public static void Add<TKey>(this Dictionary<TKey, decimal> dict1, Dictionary<TKey, decimal> dict2)
        {
            foreach (var _pair in dict2)
            {
                if (dict1.ContainsKey(_pair.Key))
                {
                    dict1[_pair.Key] += _pair.Value;
                }
                else
                {
                    dict1.Add(_pair.Key, _pair.Value);
                }
            }
        }
    }
}
