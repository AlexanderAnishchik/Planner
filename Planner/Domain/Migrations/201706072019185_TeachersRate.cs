namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeachersRate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeachersRates",
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
            DropForeignKey("dbo.TeachersRates", "RateId", "dbo.Rates");
            DropForeignKey("dbo.TeachersRates", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TeachersRates", new[] { "UserId" });
            DropIndex("dbo.TeachersRates", new[] { "RateId" });
            DropTable("dbo.TeachersRates");
        }
    }
}
