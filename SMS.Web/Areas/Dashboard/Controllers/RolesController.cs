using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using SMS.Services;
using SMS.Web.Areas.Dashboard.ViewModels;
using SMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private SMSSignInManager _signInManager;
        private SMSUserManager _userManager;
        private SMSRoleManager _roleManager;

        public SMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public SMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<SMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public SMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<SMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public RolesController()
        {
        }

        public RolesController(SMSUserManager userManager, SMSSignInManager signInManager, SMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }


        // GET: Dashboard/Roles
        public ActionResult Index(string searchTerm, int? pageNo)
        {
            int recordSize = 9;
            pageNo = pageNo ?? 1;
            RolesListingModel model = new RolesListingModel();
            model.SearchTerm = searchTerm;

            //model.Roles=

            model.Roles = SearchRoles(searchTerm, pageNo.Value, recordSize);


            var totalRecord = SearchRolesCount(searchTerm);
            model.Pager = new Pager(totalRecord, pageNo, recordSize);
            return View(model);
        }
        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int pageNo, int recordSize)
        {

            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            var skipCount = (pageNo - 1) * recordSize;
            return roles.OrderBy(x => x.Name).Skip(skipCount).Take(recordSize).ToList();


        }
        public int SearchRolesCount(string searchTerm)
        {

            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return roles.Count();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RolesActionModel model = new RolesActionModel();

            if (!string.IsNullOrEmpty(ID))//we are trying to edit record
            {
                var role = await RoleManager.FindByIdAsync(ID);
                model.ID = role.Id;
                model.Name = role.Name;


            }


            return PartialView("_Action", model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(RolesActionModel model)
        {
            IdentityResult result = null;
            JsonResult json = new JsonResult();

            if (!string.IsNullOrEmpty(model.ID)) // We try to edit record
            {
                var role = await RoleManager.FindByIdAsync(model.ID);
                role.Name = model.Name;


                result = await RoleManager.UpdateAsync(role);


            }
            else // We try to create record
            {
                var role = new IdentityRole();
                role.Name = model.Name;


                result = await RoleManager.CreateAsync(role);
            }
            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };


            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            RolesActionModel model = new RolesActionModel();

            var role = await RoleManager.FindByIdAsync(ID);

            model.ID = role.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(RolesActionModel model)
        {
            IdentityResult result = null;
            JsonResult json = new JsonResult();

            if (!string.IsNullOrEmpty(model.ID)) // We try to Delete record
            {
                var role = await RoleManager.FindByIdAsync(model.ID);

                result = await RoleManager.DeleteAsync(role);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User." };
            }


            return json;
        }
    }
}