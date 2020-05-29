using SMS.Entities;
using SMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Web.Areas.Dashboard.ViewModels
{
    public class CourseListingModel
    {
        public IEnumerable<Course> Courses { get; set; }

   
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
    public class CourseActionModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class ClassListingModel
    {
        public IEnumerable<Class> Classes { get; set; }


        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }
    public class ClassActionModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}