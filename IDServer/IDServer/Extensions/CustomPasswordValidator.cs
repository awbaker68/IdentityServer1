using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDServer.Data;
using IDServer.Models;
using Microsoft.AspNetCore.Identity;

namespace IDServer.Extensions
{
    /// <summary>
    /// Custom Password validator class
    /// </summary>
    /// <typeparam name="TUser">The user type</typeparam>
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : IdentityUser
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            // Check that the password is not the same as the email
            // The server is set to ask for email and password, if changed to ask for username
            // and password then this could be changed to check the username instead of the email
            if (string.Equals(user.Email, password, StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EnailAsPassword",
                    Description = "You cannot use your email as your password"
                }));
            }
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
