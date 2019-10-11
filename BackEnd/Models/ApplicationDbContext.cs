using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    // this class is defining how we interact with the database
    // this class inherits the DbContext class
    /// <summary> This is the connection object for our database </summary>
    public class ApplicationDbContext : DbContext
    {
        // DbContextOptions: configurations that tells this class how we connect to the db
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Speaker> Speakers { get; set; }
    }
}