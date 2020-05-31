using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Areas.Dashboard.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Dashboard/Students
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }


    }
}