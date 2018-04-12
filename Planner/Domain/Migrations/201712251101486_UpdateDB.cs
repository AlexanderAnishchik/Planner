namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FactTeachersRates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FactTeachersRates", "RateId", "dbo.Rates");
            DropForeignKey("dbo.TeachersRates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeachersRates", "RateId", "dbo.Rates");
            DropIndex("dbo.FactTeachersRates", new[] { "RateId" });
            DropIndex("dbo.FactTeachersRates", new[] { "UserId" });
            DropIndex("dbo.TeachersRates", new[] { "RateId" });
            DropIndex("dbo.TeachersRates", new[] { "UserId" });

        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FactTeachersRates",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Total = c.Int(nullable: false),
                        RateId = c.String(maxLength: 128),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.TeachersRates", "UserId");
            CreateIndex("dbo.TeachersRates", "RateId");
            CreateIndex("dbo.FactTeachersRates", "UserId");
            CreateIndex("dbo.FactTeachersRates", "RateId");
            AddForeignKey("dbo.TeachersRates", "RateId", "dbo.Rates", "Id");
            AddForeignKey("dbo.TeachersRates", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FactTeachersRates", "RateId", "dbo.Rates", "Id");
            AddForeignKey("dbo.FactTeachersRates", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
