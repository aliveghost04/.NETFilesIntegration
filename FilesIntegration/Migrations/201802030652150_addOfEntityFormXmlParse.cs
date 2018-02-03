namespace FilesIntegration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addOfEntityFormXmlParse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.XmlAccountingSeats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountSeatNumber = c.Int(nullable: false),
                        SeatDescription = c.String(),
                        SeatDate = c.DateTime(nullable: false),
                        AccountingAccount = c.Int(nullable: false),
                        MovementType = c.Int(nullable: false),
                        MovementAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.XmlAccountingSeats");
        }
    }
}
