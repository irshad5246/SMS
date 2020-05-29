using SMS.Data;
using SMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    
    public class CourseService
    {
        #region Singleton
        public static CourseService Instance
        {
            get
            {
                if (instance == null) instance = new CourseService();

                return instance;
            }
        }
        private static CourseService instance { get; set; }

        private CourseService()
        {
        }

        #endregion
        public List<Course> GetAllCourses()
        {
            var context = new SMSContext();
            return context.Courses.ToList();
        }
        public List<Course> SearchCourses(string searchTerm, int pageNo, int recordSize)
        {
            var context = new SMSContext();

            var courses = context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                courses = courses.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var skipCount = (pageNo - 1) * recordSize;
            return courses.OrderBy(x => x.Name).Skip(skipCount).Take(recordSize).ToList();

            //return accomodationTypes.ToList();
        }
        public int SearchCourseCount(string searchTerm)
        {
            var context = new SMSContext();

            var course = context.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                course = course.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }


            return course.Count();
        }
        public Course GetCoursesById(int ID)
        {
            var context = new SMSContext();
            return context.Courses.Find(ID);
        }
        public bool SaveCourses(Course course)
        {
            var context = new SMSContext();
            context.Courses.Add(course);
            return context.SaveChanges() > 0;
        }
        public bool UpdateCourses(Course course)
        {
            var context = new SMSContext();
            context.Entry(course).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }
        public bool DeleteCourses(Course course)
        {
            var context = new SMSContext();
            context.Entry(course).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}
