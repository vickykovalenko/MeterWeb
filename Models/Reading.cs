using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace MeterWeb
{
    public partial class Reading
    {
        public int ReadingId { get; set; }
        [Display(Name = "Дата подачі")]
        public DateTime? ReadingDataOfCurrentReading { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім ")]
        public int ReadingMeterId { get; set; }
        public int ReadingPaymentId { get; set; }
        [Display(Name = "Покази лічильника")]
        [RegularExpression(@"^[0-9][0-9]{4,6}$", ErrorMessage = "Некоректна довжина")]

        [Required(ErrorMessage = "Поле не повинно бути порожнім ")]
        public int ReadingNumber { get; set; }
        [Display(Name = "Номер лічильника")]
        public virtual Meter ReadingMeter { get; set; }
        public virtual Payment ReadingPayment { get; set; }
    }
}
