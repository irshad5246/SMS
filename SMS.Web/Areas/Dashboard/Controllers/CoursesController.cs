using SMS.Entities;
using SMS.Services;
using SMS.Web.Areas.Dashboard.ViewModels;
using SMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Web.Areas.Dashboard.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Dashboard/Courses
        public ActionResult Index(string searchTerm, int? courseID, int? pageNo)
        {
            CourseListingModel model = new CourseListingModel();
            int recordSize = 9;
            pageNo = pageNo ?? 1;
            model.SearchTerm = searchTerm;

            model.Courses = CourseService.Instance.SearchCourses(searchTerm, pageNo.Value, recordSize);
            var totalRecord = CourseService.Instance.SearchCourseCount(searchTerm);
            model.Pager = new Pager(totalRecord, pageNo, recordSize);

            return View(model);

        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            CourseActionModel model = new CourseActionModel();
            if (ID.HasValue)//we are trying to edit record
            {
                var course = CourseService.Instance.GetCoursesById(ID.Value);
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
        public JsonResult Action(CourseActionModel model)
        {
            var result = false;
            JsonResult json = new JsonResult();

            if (model.ID > 0) // We try to edit record
            {
                var course = CourseService.Instance.GetCoursesById(model.ID);
                course.Name = model.Name;
                course.Description = model.Description;
                result = CourseService.Instance.UpdateCourses(course);

            }
            else // We try to create record
            {
                Course course = new Course();
                course.Name = model.Name;
                course.Description = model.Description;

                result = CourseService.Instance.SaveCourses(course);
            }


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to Perform action on Course" };
            }
            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CourseActionModel model = new CourseActionModel();
            var course = CourseService.Instance.GetCoursesById(ID);
            model.ID = course.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(CourseActionModel model)
        {
            var result = false;
            JsonResult json = new JsonResult();

            var course = CourseService.Instance.GetCoursesById(model.ID);

            result = CourseService.Instance.DeleteCourses(course);


            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to Perform action on Course." };
            }
            return json;
        }
    }
}