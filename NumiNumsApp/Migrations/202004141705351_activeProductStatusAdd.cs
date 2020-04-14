namespace NumiNumsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activeProductStatusAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Status");
        }
    }
}
