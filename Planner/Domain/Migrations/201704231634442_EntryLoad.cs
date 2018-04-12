namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntryLoad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayEntryLoads", "FacultyName", c => c.String());
            AddColumn("dbo.ExtramuralEntryLoads", "F_LimitOnProjects", c => c.Double(nullable: false));
            AddColumn("dbo.ExtramuralEntryLoads", "S_LimitOnProjects", c => c.Double(nullable: false));
            Sql("UPDATE [dbo].[Departments] SET Code = 0 WHERE Code IS NULL");
            AlterColumn("dbo.Departments", "Code", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "Code", c => c.Double());
            DropColumn("dbo.ExtramuralEntryLoads", "S_LimitOnProjects");
            DropColumn("dbo.ExtramuralEntryLoads", "F_LimitOnProjects");
            DropColumn("dbo.DayEntryLoads", "FacultyName");
        }
    }
}
