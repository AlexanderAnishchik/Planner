namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FactTeachersRate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FactTeachersRates",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Total = c.Int(nullable: false),
                        RateId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Rates", t => t.RateId)
                .Index(t => t.RateId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FactTeachersRates", "RateId", "dbo.Rates");
            DropForeignKey("dbo.FactTeachersRates", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FactTeachersRates", new[] { "UserId" });
            DropIndex("dbo.FactTeachersRates", new[] { "RateId" });
            DropTable("dbo.FactTeachersRates");
        }
    }
}
