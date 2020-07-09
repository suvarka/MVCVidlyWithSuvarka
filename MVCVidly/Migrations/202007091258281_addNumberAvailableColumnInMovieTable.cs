namespace MVCVidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNumberAvailableColumnInMovieTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "NumberAvailable", c => c.Byte(nullable: false));
            Sql("Update Movies set NumberAvailable=NumberInStock");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "NumberAvailable");
        }
    }
}
