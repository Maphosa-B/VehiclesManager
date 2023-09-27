namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedadditionalattributesonvehicles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vehicles", "IsAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "IsAvailable");
            DropColumn("dbo.Drivers", "IsAvailable");
        }
    }
}
