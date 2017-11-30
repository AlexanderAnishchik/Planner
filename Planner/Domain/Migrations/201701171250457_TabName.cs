namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndivPlanFields", "TabName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndivPlanFields", "TabName");
        }
    }
}
