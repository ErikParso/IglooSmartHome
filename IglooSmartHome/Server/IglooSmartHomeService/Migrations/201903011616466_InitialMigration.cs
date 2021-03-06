namespace IglooSmartHomeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoUrl = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Sid = c.String(),
                        Provider = c.String(),
                        Salt = c.Binary(),
                        Hash = c.Binary(),
                        AccountInformationSet = c.Boolean(nullable: false),
                        Verified = c.Boolean(nullable: false),
                        ConfirmationHash = c.Binary(),
                        RefreshToken = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sid = c.String(),
                        Provider = c.String(),
                        Salt = c.Binary(),
                        Hash = c.Binary(),
                        AccountInformationSet = c.Boolean(nullable: false),
                        Verified = c.Boolean(nullable: false),
                        ConfirmationHash = c.Binary(),
                        RefreshToken = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeviceSubscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.DeviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviceSubscriptions", "DeviceId", "dbo.Devices");
            DropForeignKey("dbo.DeviceSubscriptions", "AccountId", "dbo.Accounts");
            DropIndex("dbo.DeviceSubscriptions", new[] { "DeviceId" });
            DropIndex("dbo.DeviceSubscriptions", new[] { "AccountId" });
            DropTable("dbo.DeviceSubscriptions");
            DropTable("dbo.Devices");
            DropTable("dbo.Accounts");
        }
    }
}
