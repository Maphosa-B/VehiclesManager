namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsActiveflagsinmultipletables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "IsActve", c => c.Boolean(nullable: false));
            AddColumn("dbo.Clients", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Suppliers", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Suppliers", "AddDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Suppliers", "AddDate");
            DropColumn("dbo.Suppliers", "IsActive");
            DropColumn("dbo.Clients", "IsActive");
            DropColumn("dbo.Branches", "IsActve");
        }
    }
}
