namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSapNoAndNewVisitDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BusinessDatas", "Sap", c => c.String());
            AddColumn("dbo.ClientStep8", "NewVisitDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientStep8", "NewVisitDate");
            DropColumn("dbo.BusinessDatas", "Sap");
        }
    }
}
