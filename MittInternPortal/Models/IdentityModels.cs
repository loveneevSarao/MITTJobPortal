using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MittInternPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string FullName { get; set; }
        public virtual ICollection<JobPost> JobPosts { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }
   
        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual ICollection<Employer> Employers { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Student> Student { get; set; }
        
        public DbSet<Notification> Notications { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<StudentJobPost> StudentJobPosts { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}