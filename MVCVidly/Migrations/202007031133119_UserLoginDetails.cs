namespace MVCVidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLoginDetails : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'18bcb1c1-1854-4d77-863b-0d883dc1d3c0', N'guest@vidly.com', 0, N'AInneC8FGK5Bff/93+LyV2SyknRi26J/oGdXboNNB6pliiWpPhQQpNiCM56F7bi2aA==', N'6a81947d-d824-4033-852a-cb33c1bbc19c', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'78322cc3-8374-45d2-93a2-86e28025402d', N'admin@vidly.com', 0, N'ADTJ+eeDsTLRjghtOehsHKdPlxks0IPRYcXJ276EPr8ZxcA8q6zePMd641nPDyi9Jw==', N'd434a0b6-56ab-4516-b40e-7640b8f5cde1', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'181ff356-0be3-477b-a923-78376ba82f7d', N'canManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'78322cc3-8374-45d2-93a2-86e28025402d', N'181ff356-0be3-477b-a923-78376ba82f7d')

");
        }
        
        public override void Down()
        {
        }
    }
}
