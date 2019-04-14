namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNotNullCondition : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessDatas", "RecipientName", c => c.String());
            AlterColumn("dbo.BusinessDatas", "Street", c => c.String());
            AlterColumn("dbo.BusinessDatas", "BuildingNumber", c => c.String());
            AlterColumn("dbo.BusinessDatas", "City", c => c.String());
            AlterColumn("dbo.BusinessDatas", "NIP", c => c.String());
            AlterColumn("dbo.BusinessDatas", "Owner", c => c.String());
            AlterColumn("dbo.BusinessDatas", "PhoneNumber", c => c.String());
            AlterColumn("dbo.BusinessDatas", "ContactPerson", c => c.String());
            AlterColumn("dbo.BusinessDatas", "ContactPersonPosition", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusinessDatas", "ContactPersonPosition", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "ContactPerson", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "Owner", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "NIP", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "City", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "BuildingNumber", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "Street", c => c.String(nullable: false));
            AlterColumn("dbo.BusinessDatas", "RecipientName", c => c.String(nullable: false));
        }
    }
}
