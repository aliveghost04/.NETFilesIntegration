namespace FilesIntegration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOfTableForAttachedEmployees : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        DominicanIdentification = c.String(),
                        InstitutionalEmployeeCode = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        InstitutionCode = c.String(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discounts = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeAttachments");
        }
    }
}
