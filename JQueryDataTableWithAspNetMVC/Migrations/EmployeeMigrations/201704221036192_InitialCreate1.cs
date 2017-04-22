namespace JQueryDataTableWithAspNetMVC.Migrations.EmployeeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Designation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Designation");
        }
    }
}
