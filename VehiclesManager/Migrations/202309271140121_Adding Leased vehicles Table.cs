namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingLeasedvehiclesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeasedVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.VehicleId)
                .Index(t => t.DriverId)
                .Index(t => t.BranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeasedVehicles", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.LeasedVehicles", "DriverId", "dbo.Drivers");
            DropForeignKey("dbo.LeasedVehicles", "BranchId", "dbo.Branches");
            DropIndex("dbo.LeasedVehicles", new[] { "BranchId" });
            DropIndex("dbo.LeasedVehicles", new[] { "DriverId" });
            DropIndex("dbo.LeasedVehicles", new[] { "VehicleId" });
            DropTable("dbo.LeasedVehicles");
        }
    }
}
