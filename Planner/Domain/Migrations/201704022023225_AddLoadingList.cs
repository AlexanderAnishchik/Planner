namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoadingList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoadingLists", "DepartmentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.LoadingLists", "DepartmentId");
            AddForeignKey("dbo.LoadingLists", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoadingLists", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.LoadingLists", new[] { "DepartmentId" });
            DropColumn("dbo.LoadingLists", "DepartmentId");
        }
    }
}
