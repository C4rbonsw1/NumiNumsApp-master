namespace NumiNumsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MEalDealChoices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "MealDealChoices", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "MealDealChoices");
        }
    }
}
