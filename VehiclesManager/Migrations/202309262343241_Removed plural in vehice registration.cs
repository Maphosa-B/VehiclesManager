namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removedpluralinvehiceregistration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Registration", c => c.String(nullable: false));
            DropColumn("dbo.Vehicles", "Registrations");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vehicles", "Registrations", c => c.String(nullable: false));
            DropColumn("dbo.Vehicles", "Registration");
        }
    }
}
