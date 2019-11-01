using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MittInternPortal.Models
{
    public class StudentJobPost
    {
        public int Id { get; set; }

        public int JobPostId { get; set; }
        public virtual JobPost JobPost { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}