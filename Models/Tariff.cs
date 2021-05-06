using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Вартість за 1 од. послуги")]
        public decimal TariffPrice { get; set; }
        public int TariffServiceId { get; set; }
        [Display(Name = "Вид пільги")]
        public string TariffPrivilege { get; set; }

        public virtual Service TariffService { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
 