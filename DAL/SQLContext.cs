using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class SQLContext : IdentityDbContext<User>
    {   
        public SQLContext(DbContextOptions<SQLContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Composition> Compositions { get; set; }

        public DbSet<Rating> Rating { get; set; }

        public DbSet<Tags> Tags { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

    }
}
