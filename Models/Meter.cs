using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Номер лічильника")]
        [Required(ErrorMessage = "Поле не повинно бути порожнім ")]
     
        public int MeterNumbers { get; set; }
        
        public int MeterTypeId { get; set; }
        public int MeterFlatId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім ")]
        [Display(Name = "Дата останньої повірки")]
        public DateTime MeterDataLastReplacement { get; set; }
        

        public virtual Flat MeterFlat { get; set; }
       
        public virtual MeterType MeterType { get; set; }
     

        public virtual ICollection<Reading> Readings { get; set; }
    }
}
