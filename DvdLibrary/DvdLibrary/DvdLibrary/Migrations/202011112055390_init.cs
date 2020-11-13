namespace DvdLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DvdItems",
                c => new
                    {
                        DvdId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReleaseYear = c.String(),
                        Director = c.String(),
                        RatingType = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.DvdId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DvdItems");
        }
    }
}
