using System;
using System.Collections.Generic;

#nullable disable

namespace MeterWeb
{
    public partial class Service
    {
        public Service()
        {
            MeterTypes = new HashSet<MeterType>();
            Tariffs = new HashSet<Tariff>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public virtual ICollection<MeterType> MeterTypes { get; set; }
        public virtual ICollection<Tariff> Tariffs { get; set; }
    }
}
