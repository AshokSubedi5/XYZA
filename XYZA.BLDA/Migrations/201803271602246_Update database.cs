namespace XYZA.BLDA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerModels", "Phone", c => c.String());
            AddColumn("dbo.CustomerModels", "Created_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerModels", "Modify_Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerModels", "Remarks", c => c.String());
            DropColumn("dbo.CustomerModels", "MiddleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerModels", "MiddleName", c => c.String());
            DropColumn("dbo.CustomerModels", "Remarks");
            DropColumn("dbo.CustomerModels", "Modify_Date");
            DropColumn("dbo.CustomerModels", "Created_Date");
            DropColumn("dbo.CustomerModels", "Phone");
        }
    }
}
