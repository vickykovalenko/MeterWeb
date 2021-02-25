using System;
using System.Collections.Generic;

#nullable disable

namespace MeterWeb
{
    public partial class Meter
    {
        public Meter()
        {
            Readings = new HashSet<Reading>();
        }

        public int MeterId { get; set; }
        public int MeterNumbers { get; set; }
        public int MeterTypeId { get; set; }
        public int MeterFlatId { get; set; }
        public DateTime MeterDataLastReplacement { get; set; }

        public virtual Flat MeterFlat { get; set; }
        public virtual MeterType MeterType { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
    }
}
