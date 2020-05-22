namespace SMS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclmn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "CountryID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "CountryID", c => c.String());
        }
    }
}
