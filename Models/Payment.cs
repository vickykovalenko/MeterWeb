using System;
using System.Collections.Generic;

#nullable disable

namespace MeterWeb
{
    public partial class Payment
    {
        public Payment()
        {
            Readings = new HashSet<Reading>();
        }

        public int PaymentId { get; set; }
        public DateTime PaymentDataOfCurrrentPayment { get; set; }
        public decimal PaymentSumOfCurrentMonthPayment { get; set; }
        public decimal PaymentDiscount { get; set; }
        public int PaymentTariffId { get; set; }

        public virtual Tariff PaymentTariff { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
    }
}
