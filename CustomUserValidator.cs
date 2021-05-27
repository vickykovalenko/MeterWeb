using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeterWeb.Models;
using Microsoft.AspNetCore.Identity;


namespace MeterWeb.Models
{
    public class CustomUserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (user.Email.ToLower().EndsWith("@yandex.ru")|| user.Email.ToLower().EndsWith("@mail.ru"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Даний домен знаходиться в спам-базі. Виберіть інший поштовий сервіс"
                });
            }
            if (user.UserName.Contains("admin"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Ник пользователя не должен содержать слово 'admin'"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }

    }
}

