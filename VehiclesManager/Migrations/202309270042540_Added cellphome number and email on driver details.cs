namespace VehiclesManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcellphomenumberandemailondriverdetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "EmailAddress", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Drivers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Drivers", "LastName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drivers", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Drivers", "FirstName", c => c.String(nullable: false));
            DropColumn("dbo.Drivers", "EmailAddress");
        }
    }
}
