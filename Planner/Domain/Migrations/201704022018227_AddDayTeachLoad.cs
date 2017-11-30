namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDayTeachLoad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayTeachLoads", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.DayTeachLoads", "ApplicationUserId");
            AddForeignKey("dbo.DayTeachLoads", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DayTeachLoads", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.DayTeachLoads", new[] { "ApplicationUserId" });
            DropColumn("dbo.DayTeachLoads", "ApplicationUserId");
        }
    }
}
