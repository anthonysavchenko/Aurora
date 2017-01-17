using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Services
{
    public class StringWithNumbersComparer : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            int _result;
            int _i1;
            int _i2;

            bool _isS1Numeric = Int32.TryParse(s1, out _i1);
            bool _isS2Numeric = Int32.TryParse(s2, out _i2);

            if (_isS1Numeric && _isS2Numeric)
            {
                if (_i1 > _i2)
                {
                    _result = 1;
                }
                else if (_i1 < _i2)
                {
                    _result = -1;
                }
                else
                {
                    _result = 0;
                }
            }
            else if (_isS1Numeric && !_isS2Numeric)
            {
                _result = -1;
            }
            else if (!_isS1Numeric && _isS2Numeric)
            {
                _result = 1;
            }
            else
            {
                _result = String.Compare(s1, s2, true);
            }

            return _result;
        }
    }
}
