using SMS.Data;
using SMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    
    public class ClassService
    {
        #region Singleton
        public static ClassService Instance
        {
            get
            {
                if (instance == null) instance = new ClassService();

                return instance;
            }
        }
        private static ClassService instance { get; set; }

        private ClassService()
        {
        }

        #endregion
        public List<Class> GetAllClasses()
        {
            var context = new SMSContext();
            return context.Classes.ToList();
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
        public Class GetClassesById(int ID)
        {
            var context = new SMSContext();
            return context.Classes.Find(ID);
        }
        public bool SaveClasses(Class classes)
        {
            var context = new SMSContext();
            context.Classes.Add(classes);
            return context.SaveChanges() > 0;
        }
        public bool UpdateClasses(Class classes)
        {
            var context = new SMSContext();
            context.Entry(classes).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }
        public bool Deleteclasses(Class classes)
        {
            var context = new SMSContext();
            context.Entry(classes).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}
