using HealthAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthAPI.Context
{
    public class SQLWorkshopContext:DbContext
    {
        public SQLWorkshopContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /* create composite key to reflect the DB key of Catalog */
            builder.Entity<Catalog>().HasKey(o => new { o.CourseId, o.StudentId });
        }

        public DbSet<Students> Students { get; set; }
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ToDos> ToDos { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
