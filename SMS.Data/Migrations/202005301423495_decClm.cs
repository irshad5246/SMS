namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decClm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Subjects", "TeacherID", "dbo.Teachers");
            DropIndex("dbo.Subjects", new[] { "TeacherID" });
            DropColumn("dbo.Subjects", "TeacherID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subjects", "TeacherID", c => c.Int(nullable: false));
            CreateIndex("dbo.Subjects", "TeacherID");
            AddForeignKey("dbo.Subjects", "TeacherID", "dbo.Teachers", "ID", cascadeDelete: true);
        }
    }
}
