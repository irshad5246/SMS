using SMS.Data;
using SMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Services
{
    
    public class SubjectService
    {
        #region Singleton
        public static SubjectService Instance
        {
            get
            {
                if (instance == null) instance = new SubjectService();

                return instance;
            }
        }
        private static SubjectService instance { get; set; }

        private SubjectService()
        {
        }

        #endregion
        public List<Subject> GetAllSubjects()
        {
            var context = new SMSContext();
            return context.Subjects.ToList();
        }
        public List<Subject> SearchSubjects(string searchTerm, int pageNo, int recordSize)
        {
            var context = new SMSContext();

            var courses = context.Subjects.AsQueryable();

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
        public Subject GetSubjectsById(int ID)
        {
            var context = new SMSContext();
            return context.Subjects.Find(ID);
        }
        public bool SaveSubjects(Subject subject)
        {
            var context = new SMSContext();
            context.Subjects.Add(subject);
            return context.SaveChanges() > 0;
        }
        public bool UpdateSubjects(Subject subject)
        {
            var context = new SMSContext();
            context.Entry(subject).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }
        public bool DeleteSubjects(Subject subject)
        {
            var context = new SMSContext();
            context.Entry(subject).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}
