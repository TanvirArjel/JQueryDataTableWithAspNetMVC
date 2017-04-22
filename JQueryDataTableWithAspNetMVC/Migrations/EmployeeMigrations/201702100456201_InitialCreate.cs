namespace JQueryDataTableWithAspNetMVC.Migrations.EmployeeMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        DepartmentHead = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.StaffModel",
                c => new
                    {
                        StaffId = c.Int(nullable: false, identity: true),
                        first_name = c.String(),
                        last_name = c.String(),
                        position = c.String(),
                        email = c.String(),
                        office = c.String(),
                        extn = c.Int(nullable: false),
                        age = c.Int(nullable: false),
                        salary = c.Int(nullable: false),
                        start_date = c.String(),
                    })
                .PrimaryKey(t => t.StaffId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employee", "DepartmentId", "dbo.Department");
            DropIndex("dbo.Employee", new[] { "DepartmentId" });
            DropTable("dbo.StaffModel");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
        }
    }
}
