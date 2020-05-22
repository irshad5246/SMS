using SMS.Contracts;
using SMS.Contracts.Repositories;
using SMS.Data;
using SMS.Data.Repository;
using SMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Controllers
{
    public class AJAXController : Controller
    {
        // GET: AJAX
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FillStates(int Country)
        {
            IRepositoryBase<State> statesRepo = new StatesRepository(new SMSContext());
            var states = statesRepo.GetAll().ToList().Where(s => s.CountryID == Country);

            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FillCities(int State)
        {
            IRepositoryBase<City> citiesRepo = new CitiesRepository(new SMSContext());
            var cities = citiesRepo.GetAll().ToList().Where(s => s.StateID == State);

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

    }
}
