namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIndivPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IndivPlanFieldsValues", "PlannedValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IndivPlanFieldsValues", "PlannedValue");
        }
    }
}
