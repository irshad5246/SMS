using Microsoft.AspNet.Identity;
using SMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class ProfileController : Controller
    {
        private SMSContext db = new SMSContext();
        // GET: Profile
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //instead of showing him bad request, why not show user, his own profile :D
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                string CurrentUserName = User.Identity.GetUserName();
                //id = db.Sellers.Where(s => s.Username == CurrentUserName).FirstOrDefault().ID;

                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Seller seller = db.Sellers.Find(id);
            //if (seller == null)
            //{
            //    return HttpNotFound();
            //}

            //return View(seller);
            return View();
        }

    }
}