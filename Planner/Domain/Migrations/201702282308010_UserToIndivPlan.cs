namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserToIndivPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlanTrainingJobs", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PlanScientificWorks", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PlanRemarks", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PlanMethodicalWorks", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PlanManagments", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PlanConclusions", "ApplicationUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.PlanChanges", "ApplicationUserId", c => c.String(maxLength: 128));

            CreateIndex("dbo.PlanTrainingJobs", "ApplicationUserId");
            CreateIndex("dbo.PlanScientificWorks", "ApplicationUserId");
            CreateIndex("dbo.PlanRemarks", "ApplicationUserId");
            CreateIndex("dbo.PlanMethodicalWorks", "ApplicationUserId");
            CreateIndex("dbo.PlanManagments", "ApplicationUserId");
            CreateIndex("dbo.PlanConclusions", "ApplicationUserId");
            CreateIndex("dbo.PlanChanges", "ApplicationUserId");

            AddForeignKey("dbo.PlanTrainingJobs", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanScientificWorks", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanRemarks", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanMethodicalWorks", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanManagments", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanConclusions", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanChanges", "ApplicationUserId", "dbo.ApplicationUser", "Id");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.PlanTrainingJobs", "ApplicationUserId");
            CreateIndex("dbo.PlanScientificWorks", "ApplicationUserId");
            CreateIndex("dbo.PlanRemarks", "ApplicationUserId");
            CreateIndex("dbo.PlanMethodicalWorks", "ApplicationUserId");
            CreateIndex("dbo.PlanManagments", "ApplicationUserId");
            CreateIndex("dbo.PlanConclusions", "ApplicationUserId");
            CreateIndex("dbo.PlanChanges", "ApplicationUserId");

            AddForeignKey("dbo.PlanTrainingJobs", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanScientificWorks", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanRemarks", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanMethodicalWorks", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanManagments", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanConclusions", "ApplicationUserId", "dbo.ApplicationUser", "Id");
            AddForeignKey("dbo.PlanChanges", "ApplicationUserId", "dbo.ApplicationUser", "Id");

        }
    }
}
