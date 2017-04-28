namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndivPlanToUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlanAllocations", "PlanManagmentId", "dbo.PlanManagments");
            DropForeignKey("dbo.PlanAllocations", "PlanMethodicalWorkId", "dbo.PlanMethodicalWorks");
            DropForeignKey("dbo.PlanAllocations", "PlanScientificWorkId", "dbo.PlanScientificWorks");
            DropForeignKey("dbo.PlanAllocations", "PlanTrainingJobId", "dbo.PlanTrainingJobs");
            DropForeignKey("dbo.AspNetUsers", "PlanAllocationId", "dbo.PlanAllocations");
            DropIndex("dbo.AspNetUsers", new[] { "PlanAllocationId" });
            DropIndex("dbo.PlanAllocations", new[] { "PlanTrainingJobId" });
            DropIndex("dbo.PlanAllocations", new[] { "PlanManagmentId" });
            DropIndex("dbo.PlanAllocations", new[] { "PlanMethodicalWorkId" });
            DropIndex("dbo.PlanAllocations", new[] { "PlanScientificWorkId" });
            AddColumn("dbo.AspNetUsers", "PlanPlanScientificWorkId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "PlanManagmentId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "PlanTrainingJobId", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "PlanMethodicalWorkId", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "PlanPlanScientificWorkId");
            CreateIndex("dbo.AspNetUsers", "PlanManagmentId");
            CreateIndex("dbo.AspNetUsers", "PlanTrainingJobId");
            CreateIndex("dbo.AspNetUsers", "PlanMethodicalWorkId");
            AddForeignKey("dbo.AspNetUsers", "PlanManagmentId", "dbo.PlanManagments", "Id");
            AddForeignKey("dbo.AspNetUsers", "PlanMethodicalWorkId", "dbo.PlanMethodicalWorks", "Id");
            AddForeignKey("dbo.AspNetUsers", "PlanPlanScientificWorkId", "dbo.PlanScientificWorks", "Id");
            AddForeignKey("dbo.AspNetUsers", "PlanTrainingJobId", "dbo.PlanTrainingJobs", "Id");
            DropColumn("dbo.AspNetUsers", "PlanAllocationId");
            DropTable("dbo.PlanAllocations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PlanAllocations",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        WorkTypes = c.String(),
                        PlannedVolume = c.Int(nullable: false),
                        ActualVolume = c.Int(nullable: false),
                        PlanTrainingJobId = c.String(maxLength: 128),
                        PlanManagmentId = c.String(maxLength: 128),
                        PlanMethodicalWorkId = c.String(maxLength: 128),
                        PlanScientificWorkId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "PlanAllocationId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "PlanTrainingJobId", "dbo.PlanTrainingJobs");
            DropForeignKey("dbo.AspNetUsers", "PlanPlanScientificWorkId", "dbo.PlanScientificWorks");
            DropForeignKey("dbo.AspNetUsers", "PlanMethodicalWorkId", "dbo.PlanMethodicalWorks");
            DropForeignKey("dbo.AspNetUsers", "PlanManagmentId", "dbo.PlanManagments");
            DropIndex("dbo.AspNetUsers", new[] { "PlanMethodicalWorkId" });
            DropIndex("dbo.AspNetUsers", new[] { "PlanTrainingJobId" });
            DropIndex("dbo.AspNetUsers", new[] { "PlanManagmentId" });
            DropIndex("dbo.AspNetUsers", new[] { "PlanPlanScientificWorkId" });
            DropColumn("dbo.AspNetUsers", "PlanMethodicalWorkId");
            DropColumn("dbo.AspNetUsers", "PlanTrainingJobId");
            DropColumn("dbo.AspNetUsers", "PlanManagmentId");
            DropColumn("dbo.AspNetUsers", "PlanPlanScientificWorkId");
            CreateIndex("dbo.PlanAllocations", "PlanScientificWorkId");
            CreateIndex("dbo.PlanAllocations", "PlanMethodicalWorkId");
            CreateIndex("dbo.PlanAllocations", "PlanManagmentId");
            CreateIndex("dbo.PlanAllocations", "PlanTrainingJobId");
            CreateIndex("dbo.AspNetUsers", "PlanAllocationId");
            AddForeignKey("dbo.AspNetUsers", "PlanAllocationId", "dbo.PlanAllocations", "Id");
            AddForeignKey("dbo.PlanAllocations", "PlanTrainingJobId", "dbo.PlanTrainingJobs", "Id");
            AddForeignKey("dbo.PlanAllocations", "PlanScientificWorkId", "dbo.PlanScientificWorks", "Id");
            AddForeignKey("dbo.PlanAllocations", "PlanMethodicalWorkId", "dbo.PlanMethodicalWorks", "Id");
            AddForeignKey("dbo.PlanAllocations", "PlanManagmentId", "dbo.PlanManagments", "Id");
        }
    }
}
