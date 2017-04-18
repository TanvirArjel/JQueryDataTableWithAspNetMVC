using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace JQueryDataTableWithAspNetMVC.Models
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("EmployeeConnection")
        {
            Database.SetInitializer( new DropCreateDatabaseIfModelChanges<EmployeeDbContext>());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<StaffModel> StaffModels { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<JQueryDataTableWithAspNetMVC.Models.Department> Departments { get; set; }
    }
}