namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false),
                        Registrations = c.String(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        SupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            DropTable("dbo.SupplierEntities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SupplierEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Vehicles", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.Vehicles", new[] { "SupplierId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Suppliers");
        }
    }
}
