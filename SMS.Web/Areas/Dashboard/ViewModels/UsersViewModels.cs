using Microsoft.AspNet.Identity.EntityFramework;
using SMS.Entities;
using SMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMS.Web.Areas.Dashboard.ViewModels
{
    public class UsersListingModel
    {
        public IEnumerable<SMSUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string RoleID { get; set; }

        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }
    public class UsersActionModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        [Display(Name = "Country")]
        public int CountryID { get; set; }
        [Display(Name = "State")]
        public int StateID { get; set; }
        [Display(Name = "City")]
        public int CityID { get; set; }
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual City City { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }

        //Lists for DropDowns
        public virtual IEnumerable<Country> CountriesList { get; set; }
        public virtual IEnumerable<State> StatesList { get; set; }
        public virtual IEnumerable<City> CitiesList { get; set; }

    }
    public class UsersDetailsModel
    {
       

        public SMSUser User { get; set; }
    }
    public class UserRolesModel

    {
        public string UserID { get; set; }
        public IEnumerable<IdentityRole> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
