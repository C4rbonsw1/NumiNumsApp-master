namespace NumiNumsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unknownmodelchange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "MealDealChoices", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "MealDealChoices", c => c.String(nullable: false, maxLength: 1000));
        }
    }
}
