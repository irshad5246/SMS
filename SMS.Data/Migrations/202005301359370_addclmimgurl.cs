namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclmimgurl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "ImageURL", c => c.String());
            AddColumn("dbo.Subjects", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "Description");
            DropColumn("dbo.Students", "ImageURL");
        }
    }
}
