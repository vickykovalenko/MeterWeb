using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace MeterWeb
{
    public partial class Flat
    {
        public Flat()
        {
            Meters = new HashSet<Meter>();
        }

        public int FlatId { get; set; }
        [Required(ErrorMessage = "Поле не повинно бути порожнім ")]
        [Display(Name = "Адреса")]
        public string FlatAddress { get; set; }
   

        public virtual ICollection<Meter> Meters { get; set; }
    }
}
