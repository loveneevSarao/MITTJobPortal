using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MittInternPortal.Models
{
    public class JobManagement
    {
        ApplicationDbContext db;
        public JobManagement(ApplicationDbContext db)
        {
            this.db = db;
        }
        public ApplicationUser CheckUserId(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    return user;
                }
            }
            return null; // Or throw an exception;
        }
        public JobPost CheckJobId(int jobId)
        {
            var job = db.JobPosts.Find(jobId);
            if (job != null)
            {
                return job;
            }
            return null; // Or throw an exception;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ICollection<JobPost> GetUserTickets(string userId)
        {
            var user = CheckUserId(userId);
            if (user != null)
            {
                return user.JobPosts;
            }
            return null; // or throw and exceptions like HttpNotFound
        }
        //public ICollection<Ticket> GetTicketUsers(int 
    }
}