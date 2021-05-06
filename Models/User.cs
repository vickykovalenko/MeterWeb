using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;



namespace MeterWeb.Models
{
    public class User : IdentityUser
    {
        //public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        //public string Email { get; set; }

    }
}
