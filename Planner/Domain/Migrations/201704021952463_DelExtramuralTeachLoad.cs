namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelExtramuralTeachLoad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ExtramuralTeachLoadId", "dbo.ExtramuralTeachLoads");
            DropIndex("dbo.AspNetUsers", new[] { "ExtramuralTeachLoadId" });
            RenameColumn(table: "dbo.AspNetUsers", name: "DayTeachLoadId", newName: "DayTeachLoad_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_DayTeachLoadId", newName: "IX_DayTeachLoad_Id");
            DropColumn("dbo.AspNetUsers", "ExtramuralTeachLoadId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ExtramuralTeachLoadId", c => c.String(maxLength: 128));
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_DayTeachLoad_Id", newName: "IX_DayTeachLoadId");
            RenameColumn(table: "dbo.AspNetUsers", name: "DayTeachLoad_Id", newName: "DayTeachLoadId");
            CreateIndex("dbo.AspNetUsers", "ExtramuralTeachLoadId");
            AddForeignKey("dbo.AspNetUsers", "ExtramuralTeachLoadId", "dbo.ExtramuralTeachLoads", "Id");
        }
    }
}
