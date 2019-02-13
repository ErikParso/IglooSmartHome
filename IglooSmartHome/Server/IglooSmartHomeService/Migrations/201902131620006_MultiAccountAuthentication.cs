namespace IglooSmartHomeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MultiAccountAuthentication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "Email", c => c.String());
            AddColumn("dbo.Accounts", "AccountInformationSet", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Accounts", "Provider", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accounts", "Provider", c => c.Int(nullable: false));
            DropColumn("dbo.Accounts", "AccountInformationSet");
            DropColumn("dbo.Accounts", "Email");
        }
    }
}
