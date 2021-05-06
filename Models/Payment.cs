using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Дата оплати")]

        public DateTime PaymentDataOfCurrrentPayment { get; set; }
        [Display(Name = "Сума оплати")]
        public decimal PaymentSumOfCurrentMonthPayment { get; set; }
        [Display(Name = "Знижка")]
        public decimal PaymentDiscount { get; set; }
        [Display(Name = "Вид пільги")]
        public int PaymentTariffId { get; set; }
        [Display(Name = "Вид пільги")]
        public virtual Tariff PaymentTariff { get; set; }
        public virtual ICollection<Reading> Readings { get; set; }
        public decimal calculated
        {
            get
            {
                return (decimal)(PaymentDiscount * 2);
            }
        }
    }
}
