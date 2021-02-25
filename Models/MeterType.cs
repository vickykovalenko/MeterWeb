using System;
using System.Collections.Generic;

#nullable disable

namespace MeterWeb
{
    public partial class MeterType
    {
        public MeterType()
        {
            Meters = new HashSet<Meter>();
        }

        public int MeterTypeId { get; set; }
        public string MeterTypeName { get; set; }
        public int MeterServiceId { get; set; }

        public virtual Service MeterService { get; set; }
        public virtual ICollection<Meter> Meters { get; set; }
    }
}
