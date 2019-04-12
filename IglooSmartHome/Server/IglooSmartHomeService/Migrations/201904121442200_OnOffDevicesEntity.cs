namespace IglooSmartHomeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OnOffDevicesEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OnOffDevices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DeviceId = c.Int(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.DeviceId, cascadeDelete: true)
                .Index(t => t.DeviceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OnOffDevices", "DeviceId", "dbo.Devices");
            DropIndex("dbo.OnOffDevices", new[] { "DeviceId" });
            DropTable("dbo.OnOffDevices");
        }
    }
}
