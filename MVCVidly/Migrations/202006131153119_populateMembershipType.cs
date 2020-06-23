namespace MVCVidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateMembershipType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,DiscountRate) VALUES (1,'Pay as you Go',0,0,0)");
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,DiscountRate) VALUES (2,'Monthly',30,1,10)");
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,DiscountRate) VALUES (3,'Quaterly',90,3,15)");
            Sql("INSERT INTO MembershipTypes (Id,Name,SignUpFee,DurationInMonth,DiscountRate) VALUES (4,'Yearly',300,12,20)");
        }
        
        public override void Down()
        {
        }
    }
}
