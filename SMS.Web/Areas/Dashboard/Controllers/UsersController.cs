using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SMS.Contracts.Repositories;
using SMS.Data;
using SMS.Data.Repository;
using SMS.Entities;
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
    public class UsersController : Controller
    {
        SMSContext myDataContext = new SMSContext();
        IRepositoryBase<Country> Countries;

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

        public UsersController()
        {
            Countries = new CountriesRepository(myDataContext);
        }

        public UsersController(SMSUserManager userManager, SMSSignInManager signInManager, SMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }


        // GET: Dashboard/Users
        public ActionResult Index(string searchTerm, string roleID, int? pageNo)
        {
            int recordSize = 9;
            pageNo = pageNo ?? 1;
            UsersListingModel model = new UsersListingModel();
            model.SearchTerm = searchTerm;
            model.RoleID = roleID;
           model.Roles = RoleManager.Roles.ToList();

            model.Users = SearchUsers(searchTerm, roleID, pageNo.Value, recordSize);


            var totalRecord = SearchUsersCount(searchTerm, roleID);
            model.Pager = new Pager(totalRecord, pageNo, recordSize);
            return View(model);
        }
        public IEnumerable<SMSUser> SearchUsers(string searchTerm, string roleID, int pageNo, int recordSize)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
                //or this code  and change method async and task in IEnumerable<HMSUser>SearchUsers  ,index ,SearchUsersCount
                //var role = await RoleManager.FindByIdAsync(roleID);
                //var userIDs = role.Users.Select(x => x.UserId).Tolist();
                //users = users.Where(x => userIDs.Contains(x.Id));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                users = users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleID));
            }

            var skipCount = (pageNo - 1) * recordSize;
            return users.OrderBy(x => x.UserName).Skip(skipCount).Take(recordSize).ToList();
        }
        public int SearchUsersCount(string searchTerm, string roleID)
        {

            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                users = users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleID));
            }


            return users.Count();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UsersActionModel model = new UsersActionModel();
            model.Country = Countries.GetByID(166);

            model.CountriesList = Countries.GetAll();


            //model.AvailableAccomodationPackage = AccomodationPackagesService.Instance.GetAllAccomodationPackages();
            if (!string.IsNullOrEmpty(ID))//we are trying to edit record
            {
                var user = await UserManager.FindByIdAsync(ID);

                model.ID = user.Id;
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.Username = user.UserName;
                model.PhoneNumber = user.PhoneNumber;
                model.CountryID = user.CountryID;
                model.StateID = user.StateID;
                model.CityID = user.CityID;
                //model.Country = user.Country;
                //model.City = user.City;
                model.Address = user.Address;


            }


            return PartialView("_Action", model);
        }
        [HttpPost]
        public async Task<JsonResult> Action(UsersActionModel model)
        {
            IdentityResult result = null;
            JsonResult json = new JsonResult();
            model.Country = Countries.GetByID(166);

            model.CountriesList = Countries.GetAll();

            if (!string.IsNullOrEmpty(model.ID)) // We try to edit record
            {
                var user = await UserManager.FindByIdAsync(model.ID);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.PhoneNumber = model.PhoneNumber;
                user.CountryID = model.CountryID;
                user.StateID = model.StateID;
                user.CityID = model.CityID;
                user.Address = model.Address;

                result = await UserManager.UpdateAsync(user);


            }
            else // We try to create record
            {
                var user = new SMSUser();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.PhoneNumber = model.PhoneNumber;
                user.CountryID = model.CountryID;
                user.StateID = model.StateID;
                user.CityID = model.CityID;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }
            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };


            return json;
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            UsersActionModel model = new UsersActionModel();

            var user = await UserManager.FindByIdAsync(ID);

            model.ID = user.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UsersActionModel model)
        {
            IdentityResult result = null;
            JsonResult json = new JsonResult();

            if (!string.IsNullOrEmpty(model.ID)) // We try to Delete record
            {
                var user = await UserManager.FindByIdAsync(model.ID);

                result = await UserManager.DeleteAsync(user);
                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User." };
            }


            return json;
        }

        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {

            UserRolesModel model = new UserRolesModel();

            // model.Roles = RoleManager.Roles.ToList();
            model.UserID = ID;
            var user = await UserManager.FindByIdAsync(ID);
            var userRoleIDs = user.Roles.Select(x => x.RoleId).ToList();

            model.UserRoles = RoleManager.Roles.Where(x => userRoleIDs.Contains(x.Id)).ToList();
            model.Roles = RoleManager.Roles.Where(x => !userRoleIDs.Contains(x.Id)).ToList();
            return PartialView("_UserRoles", model);
        }

        [HttpPost]
        public async Task<JsonResult> UserRolesOperation(string userID, string roleID, bool isDelete = false)
        {
            IdentityResult result = null;
            JsonResult json = new JsonResult();

            var user = await UserManager.FindByIdAsync(userID);
            var role = await RoleManager.FindByIdAsync(roleID);

            if (user != null && role != null)
            {
                if (!isDelete)
                {
                    result = await UserManager.AddToRoleAsync(userID, role.Name);
                }
                else
                {
                    result = await UserManager.RemoveFromRoleAsync(userID, role.Name);
                }

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid operation." };
            }

            return json;
        }
    }
}