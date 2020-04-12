namespace NumiNumsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addProductType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductVType_ProductTypeId", c => c.Int());
            CreateIndex("dbo.Products", "ProductVType_ProductTypeId");
            AddForeignKey("dbo.Products", "ProductVType_ProductTypeId", "dbo.ProductTypes", "ProductTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductVType_ProductTypeId", "dbo.ProductTypes");
            DropIndex("dbo.Products", new[] { "ProductVType_ProductTypeId" });
            DropColumn("dbo.Products", "ProductVType_ProductTypeId");
        }
    }
}
