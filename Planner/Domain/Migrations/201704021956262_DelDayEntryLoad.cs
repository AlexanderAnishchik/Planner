namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelDayEntryLoad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "DayTeachLoad_Id", "dbo.DayTeachLoads");
            DropIndex("dbo.AspNetUsers", new[] { "DayTeachLoad_Id" });
            DropColumn("dbo.AspNetUsers", "DayTeachLoad_Id");
            //Sql("ALTER TABLE [dbo].[AspNetUsers] DROP CONSTRAINT [FK_dbo.AspNetUsers_dbo.DayTeachLoads_DayTeachLoadId]");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DayTeachLoad_Id", "dbo.DayTeachLoads");
            DropIndex("dbo.AspNetUsers", new[] { "DayTeachLoad_Id" });
            DropColumn("dbo.AspNetUsers", "DayTeachLoad_Id");
        }
    }
}
