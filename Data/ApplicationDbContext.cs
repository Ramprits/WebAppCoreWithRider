using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppCore.Entities;

namespace WebAppCore.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet <Department> Departments { get; set; }

        public DbSet <Employee> Employees { get; set; }

        public DbSet <Camp> Camps { get; set; }

        public DbSet <Speaker> Speakers { get; set; }

        public DbSet <Talk> Talks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity <Camp>().Property(c => c.Moniker).IsRequired();

            builder.Entity <Camp>()
                .Property(c => c.RowVersion)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            builder.Entity <Speaker>()
                .Property(c => c.RowVersion)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();

            builder.Entity <Talk>()
                .Property(c => c.RowVersion)
                .ValueGeneratedOnAddOrUpdate()
                .IsConcurrencyToken();
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
