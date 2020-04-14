namespace NumiNumsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MEalDealChoicesorderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "MealDealChoices", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "MealDealChoices");
        }
    }
}
