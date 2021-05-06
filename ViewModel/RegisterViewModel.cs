using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MeterWeb.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Прізвище")]
        public string SecondName { get; set; }

        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "По-батькові")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Електронна пошта")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [Display(Name = "Підтвердження пароля")]
        [DataType(DataType.Password)]

        public string PasswordConfirm { get; set; }














    }
}
