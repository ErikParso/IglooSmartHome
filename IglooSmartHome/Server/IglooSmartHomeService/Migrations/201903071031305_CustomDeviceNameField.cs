namespace IglooSmartHomeService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomDeviceNameField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Devices", "CustomDeviceName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Devices", "CustomDeviceName");
        }
    }
}
