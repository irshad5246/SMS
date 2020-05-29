using SMS.Entities;
using SMS.Services;
using SMS.Web.Areas.Dashboard.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Areas.Dashboard.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Dashboard/Classes
        public ActionResult Index()
        {
            ClassListingModel model = new ClassListingModel();

            model.Classes = ClassService.Instance.GetAllClasses();
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            ClassActionModel model = new ClassActionModel();
            if (ID.HasValue)//we are trying to edit record
            {
                var course = ClassService.Instance.GetClassesById(ID.Value);
                model.ID = course.ID;
                model.Name = course.Name;
                model.Description = course.Description;
            }
            //else no need
            //{

            //}

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(ClassActionModel model)
        {
            var result = false;
            JsonResult json = new JsonResult();

            if (model.ID > 0) // We try to edit record
            {
                var course = ClassService.Instance.GetClassesById(model.ID);
                course.Name = model.Name;
                course.Description = model.Description;
                result = ClassService.Instance.UpdateClasses(course);

            }
            else // We try to create record
            {
                Class course = new Class();
                course.Name = model.Name;
                course.Description = model.Description;

                result = ClassService.Instance.SaveClasses(course);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to Perform action on Classes" };
            }
            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            ClassActionModel model = new ClassActionModel();
            var course = ClassService.Instance.GetClassesById(ID);
            model.ID = course.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(ClassActionModel model)
        {
            var result = false;
            JsonResult json = new JsonResult();

            var course = ClassService.Instance.GetClassesById(model.ID);

            result = ClassService.Instance.Deleteclasses(course);


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to Perform action on Classes." };
            }
            return json;
        }
    }
}