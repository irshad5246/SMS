using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities
{
    public class Teacher
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CourseID { get; set; }

        public virtual Course Course { get; set; }
    }
}
