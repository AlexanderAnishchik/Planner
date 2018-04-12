namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExtramuralTeachLoad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExtramuralTeachLoads", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ExtramuralTeachLoads", "ApplicationUserId");
            AddForeignKey("dbo.ExtramuralTeachLoads", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtramuralTeachLoads", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.ExtramuralTeachLoads", new[] { "ApplicationUserId" });
            DropColumn("dbo.ExtramuralTeachLoads", "ApplicationUserId");
        }
    }
}
