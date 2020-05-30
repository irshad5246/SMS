namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableSTS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EnrolementNo = c.String(),
                        Name = c.String(),
                        FatherName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Dateofjoining = c.DateTime(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        CourseID = c.Int(nullable: false),
                        ClassID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classes", t => t.ClassID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.ClassID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Teachers", t => t.TeacherID, cascadeDelete: true)
                .Index(t => t.TeacherID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "TeacherID", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Students", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Students", "ClassID", "dbo.Classes");
            DropIndex("dbo.Teachers", new[] { "CourseID" });
            DropIndex("dbo.Subjects", new[] { "TeacherID" });
            DropIndex("dbo.Students", new[] { "ClassID" });
            DropIndex("dbo.Students", new[] { "CourseID" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Subjects");
            DropTable("dbo.Students");
        }
    }
}
