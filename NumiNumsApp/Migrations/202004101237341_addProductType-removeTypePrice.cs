namespace NumiNumsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductTyperemoveTypePrice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductTypes", "TypePrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductTypes", "TypePrice", c => c.Double(nullable: false));
        }
    }
}
