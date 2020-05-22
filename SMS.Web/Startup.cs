using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SMS.Data;
using SMS.Entities;

[assembly: OwinStartupAttribute(typeof(SMS.Web.Startup))]
namespace SMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        private void createRolesandUsers()
        {
            SMSContext context = new SMSContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<SMSUser>(new UserStore<SMSUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Manager"))
            {
                var role2 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role2.Name = "Manager";
                roleManager.Create(role2);
            }

            // creating Creating Seller role    
            if (!roleManager.RoleExists("User"))
            {
                var role3 = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role3.Name = "User";
                roleManager.Create(role3);
            }
        }
    }
}