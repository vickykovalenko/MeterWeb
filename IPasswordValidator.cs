using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;



namespace MeterWeb
{
    interface IPasswordValidator <T> where T : class
    { 
        Task<IdentityResult> ValidateAsync(UserManager<T> manager, T user, string password);
    }
}
