using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using SMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
   
    public class SMSSignInManager : SignInManager<SMSUser, string>
    {
        public SMSSignInManager(SMSUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(SMSUser user)
        {
            return user.GenerateUserIdentityAsync((SMSUserManager)UserManager);
        }

        public static SMSSignInManager Create(IdentityFactoryOptions<SMSSignInManager> options, IOwinContext context)
        {
            return new SMSSignInManager(context.GetUserManager<SMSUserManager>(), context.Authentication);
        }
    }
}


