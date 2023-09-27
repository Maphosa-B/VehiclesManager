namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedcommentattributeonLeasedVehicle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LeasedVehicles", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LeasedVehicles", "Comment");
        }
    }
}
