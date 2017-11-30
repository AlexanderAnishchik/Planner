namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DEK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanTrainingJobs", "PlannedDEK", c => c.Int(nullable: false));
            AddColumn("dbo.PlanTrainingJobs", "DoneDEK", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlanTrainingJobs", "DoneDEK");
            DropColumn("dbo.PlanTrainingJobs", "PlannedDEK");
        }
    }
}
