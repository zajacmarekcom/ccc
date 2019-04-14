namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedNotNullCondition2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BusinessDatas", "ProvinceId", c => c.Int());
            AlterColumn("dbo.BusinessDatas", "DistrictId", c => c.Int());
            AlterColumn("dbo.BusinessDatas", "LegalFormId", c => c.Int());
            AlterColumn("dbo.BusinessDatas", "GroupId", c => c.Int());
            AlterColumn("dbo.BusinessDatas", "AgentId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BusinessDatas", "AgentId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusinessDatas", "GroupId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusinessDatas", "LegalFormId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusinessDatas", "DistrictId", c => c.Int(nullable: false));
            AlterColumn("dbo.BusinessDatas", "ProvinceId", c => c.Int(nullable: false));
        }
    }
}
