namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableNewVisitDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientStep8", "NewVisitDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientStep8", "NewVisitDate", c => c.DateTime(nullable: false));
        }
    }
}
