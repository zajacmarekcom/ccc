namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostCodes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostCodes",
                c => new
                    {
                        PostCodeNumber = c.String(nullable: false, maxLength: 128),
                        City = c.String(),
                        ProvinceId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostCodeNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PostCodes");
        }
    }
}
