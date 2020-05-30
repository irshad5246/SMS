using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities
{
   public class Student
    {
        public int ID { get; set; }

        public string EnrolementNo { get; set; }

        public string Name { get; set; }

        public string FatherName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime DOB { get; set; }

        public DateTime Dateofjoining { get; set; }

        public string ImageURL { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Address { get; set; }

        public int CourseID { get; set; }

        public virtual Course Course { get; set; }

        public int ClassID { get; set; }

        public virtual Class Class { get; set; }

    }
}
