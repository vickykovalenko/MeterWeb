using System;
using System.Collections.Generic;

#nullable disable

namespace MeterWeb
{
    public partial class Tariff
    {
        public Tariff()
        {
            Payments = new HashSet<Payment>();
        }

        public int TariffId { get; set; }
        public decimal TariffPrice { get; set; }
        public int TariffServiceId { get; set; }
        public string TariffPrivilege { get; set; }

        public virtual Service TariffService { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
