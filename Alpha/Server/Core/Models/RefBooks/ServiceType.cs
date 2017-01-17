using System;
using System.Collections.Generic;

namespace Taumis.Alpha.Server.Core.Models.RefBooks
{
    public class ServiceType : Entity
    {
        public ServiceType()
        {
            Services = new List<Service>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}