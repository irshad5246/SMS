using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Entities
{
    public class Image
    {
        public int ID { get; set; }

        [StringLength(255)]
        [Required]
        [Display(Name = "Image Path is Required.")]
        public string URL { get; set; }
    }

   
}
