namespace IglooSmartHomeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefreshTokenColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "RefreshToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "RefreshToken");
        }
    }
}
