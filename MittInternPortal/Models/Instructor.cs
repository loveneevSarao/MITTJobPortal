using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MittInternPortal.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser InstructorName { get; set; }
        
        public string Address { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public int Contact { get; set; }
        public string InstructorImagePath { get; set; }

    }
}