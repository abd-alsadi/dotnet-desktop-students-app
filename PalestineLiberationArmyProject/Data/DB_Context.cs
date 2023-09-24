using Microsoft.EntityFrameworkCore;
using PalestineLiberationArmyProject.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalestineLiberationArmyProject.Data
{
    public class DB_Context:DbContext
    {
        public DbSet<clsEntity> Students { get; set; }
     

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=PalestineLiberationArmyProject;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
