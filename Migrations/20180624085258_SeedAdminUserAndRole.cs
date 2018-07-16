using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace zkiosk.Migrations
{
    public partial class SeedAdminUserAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
	                VALUES ('cf14c7cf-4b39-41c2-b8c5-29bf30e0341a', 'f22ac560-b640-4108-91fc-9e461dfa8f68', 'Admin', 'ADMIN')
            ");

            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed],
                        [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber],
                        [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
                    VALUES ('dc51c263-7afa-432c-9409-7816eba4d6a1', 0, 'ca4aaf5f-7732-4ba8-9aef-92a481d26265', 'admin@admin.com', 
                        0, 1, NULL, 'ADMIN@ADMIN.COM', 'ADMIN@ADMIN.COM', 'AQAAAAEAACcQAAAAEFgTf+ttRI6F1fFgdOKxS8dvKLtVnw+XLDGiB+4U8RAFsQRht7a1WkKG3hktzFieig==', 
                        NULL, 0, '40d54a94-dfc4-4972-80f4-abd6b358c1d9', 0, 'admin@admin.com')
            ");

            migrationBuilder.Sql(@"
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId])
                    VALUES ('dc51c263-7afa-432c-9409-7816eba4d6a1', 'cf14c7cf-4b39-41c2-b8c5-29bf30e0341a')
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM [dbo].[AspNetUserRoles]
                WHERE ([UserId] = 'dc51c263-7afa-432c-9409-7816eba4d6a1' AND
                    [RoleId] = 'cf14c7cf-4b39-41c2-b8c5-29bf30e0341a')
            ");

            migrationBuilder.Sql(@"DELETE FROM [dbo].[AspNetUsers] WHERE [Id] = 'dc51c263-7afa-432c-9409-7816eba4d6a1'");

            migrationBuilder.Sql(@"DELETE FROM [dbo].[AspNetRoles] WHERE [Id] = 'cf14c7cf-4b39-41c2-b8c5-29bf30e0341a'");
        }
    }
}
