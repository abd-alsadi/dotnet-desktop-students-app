using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PalestineLiberationArmyProject.Models;
using PalestineLiberationArmyProject.Models.Core;
using System.Data;

namespace PalestineLiberationArmyProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private ApplicationDbContext()
        {

        }
        private static ApplicationDbContext ___instance { set; get; }
        public DbSet<clsEntity> Entity { set; get; }
        public DbSet<clsEntityField> EntityField { set; get; }

        public DbSet<clsEntityFieldValue> EntityFieldValue { set; get; }

        public DbSet<clsEntityInstance> EntityInstance { set; get; }

        public DbSet<clsEntityPermisionRole> EntityPermisionRole { set; get; }

        public DbSet<clsForm> Form { set; get; }

        public DbSet<clsFrmField> FormField { set; get; }

        public DbSet<clsFrmFieldValue> FormFieldValue { set; get; }

        public DbSet<clsFrmInstance> FormInstance { set; get; }

        public DbSet<clsFrmPermisionRole> FormPermisionRole { set; get; }
        public DbSet<clsRole> Role { set; get; }
        public DbSet<clsUser> User { set; get; }
        public DbSet<clsPerson> Person { set; get; }

        public DbSet<clsTransaction> Transaction { set; get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public static ApplicationDbContext getInstance()
        {
            if (___instance == null)
            {
                var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(@"Server=.;Database=PalestineLiberationArmyProject;Trusted_Connection=True;MultipleActiveResultSets=true").Options;

                 ___instance = new ApplicationDbContext(contextOptions);
            }
            return ___instance;
        }
    }
}
