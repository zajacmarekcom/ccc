namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class producerGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Producers", "Group", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Producers", "Group");
        }
    }
}
