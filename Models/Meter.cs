using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


#nullable disable

namespace MeterWeb
{
    public partial class Meter
    {
        public Meter()
        {
            Readings = new HashSet<Reading>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MeterId { get; set; }
        [Display(Name = "Номер лічильника")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім ")]
        
        [RegularExpression(@"^[0-9][0-9]{5,7}$", ErrorMessage = "Некоректна довжина")]

        public int MeterNumbers { get; set; }
        [Display(Name = "Тип лічильника")]
        public int MeterTypeId { get; set; }
        [Display(Name = "Адреса")]
        public int MeterFlatId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім ")]
        [Display(Name = "Дата останньої повірки")]
        public DateTime MeterDataLastReplacement { get; set; }

        [Display(Name = "Адреса")]
        public virtual Flat MeterFlat { get; set; }
        [Display(Name = "Тип лічильника")]
        public virtual MeterType MeterType { get; set; }
     

        public virtual ICollection<Reading> Readings { get; set; }
    }
}
