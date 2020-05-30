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
    public class SubjectsController : Controller
    {
        // GET: Dashboard/Subjects
        public ActionResult Index()
        {
            SubjectListingModel model = new SubjectListingModel();

            model.Subjects = SubjectService.Instance.GetAllSubjects();
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            SubjectActionModel model = new SubjectActionModel();
            if (ID.HasValue)//we are trying to edit record
            {
                var subject = SubjectService.Instance.GetSubjectsById(ID.Value);
                model.ID = subject.ID;
                model.Name = subject.Name;
                model.Description = subject.Description;
            }
            //else no need
            //{

            //}

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(SubjectActionModel model)
        {
            var result = false;
            JsonResult json = new JsonResult();

            if (model.ID > 0) // We try to edit record
            {
                var subject = SubjectService.Instance.GetSubjectsById(model.ID);
                subject.Name = model.Name;
                subject.Description = model.Description;
                result = SubjectService.Instance.UpdateSubjects(subject);

            }
            else // We try to create record
            {
                Subject subject = new Subject();
                subject.Name = model.Name;
                subject.Description = model.Description;

                result = SubjectService.Instance.SaveSubjects(subject);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to Perform action on Subject" };
            }
            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            SubjectActionModel model = new SubjectActionModel();
            var subject = SubjectService.Instance.GetSubjectsById(ID);
            model.ID = subject.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(SubjectActionModel model)
        {
            var result = false;
            JsonResult json = new JsonResult();

            var subject = SubjectService.Instance.GetSubjectsById(model.ID);

            result = SubjectService.Instance.DeleteSubjects(subject);


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to Perform action on Subjects." };
            }
            return json;
        }
    }
}