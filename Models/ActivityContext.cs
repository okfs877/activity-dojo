using Microsoft.EntityFrameworkCore;
 
namespace CBeltExam.Models
{
    public class ActivityContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options) { }
        public DbSet<ModelUser> Users {get;set;}
        public DbSet<ModelActivity> Activities {get;set;}
        public DbSet<RSVP> RSVPs {get;set;}
    }
}