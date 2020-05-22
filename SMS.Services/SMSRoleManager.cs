using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
   
    public class SMSRoleManager : RoleManager<IdentityRole>
    {
        public SMSRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {

        }
        public static SMSRoleManager Create(IdentityFactoryOptions<SMSRoleManager> options, IOwinContext context)
        {
            return new SMSRoleManager(new RoleStore<IdentityRole>(context.Get<SMSContext>()));
        }
    }
}
