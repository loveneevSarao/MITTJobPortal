
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MittInternPortal.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int Contact { get; set; }

        public string InstructorId { get; set; }
        public virtual ApplicationUser Instructor { get; set; }

        public virtual ICollection<StudentJobPost> StudentJobPosts { get; set; }
        [DisplayName("Upload Image")]
        public string StudentImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}