using System;
using System.Collections.Generic;

#nullable disable

namespace MeterWeb
{
    public partial class Reading
    {
        public int ReadingId { get; set; }
        public DateTime? ReadingDataOfCurrentReading { get; set; }
        public int ReadingMeterId { get; set; }
        public int ReadingPaymentId { get; set; }

        public virtual Meter ReadingMeter { get; set; }
        public virtual Payment ReadingPayment { get; set; }
    }
}
