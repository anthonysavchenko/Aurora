﻿namespace Taumis.Alpha.WinClient.Aurora.Modules.Accounting.Counters.Models
{
    public class CounterInfo
    {
        public string Model { get; set; }
        public string Number { get; set; }
        public int ServiceId { get; set; }

        public CustomerData CustomerData { get; set; }
    }
}
