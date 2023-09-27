namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedadddateinLeasedvehicleentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeasedVehicles", "AddDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeasedVehicles", "AddDate");
        }
    }
}
