namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedadditionalattributesonLeasedVehicleEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeasedVehicles", "IsReturned", c => c.Boolean(nullable: false));
            AddColumn("dbo.LeasedVehicles", "ReturnDate", c => c.DateTime());
            AddColumn("dbo.LeasedVehicles", "ConditionStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeasedVehicles", "ConditionStatus");
            DropColumn("dbo.LeasedVehicles", "ReturnDate");
            DropColumn("dbo.LeasedVehicles", "IsReturned");
        }
    }
}
