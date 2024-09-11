using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_CRM_Infrastructure.Migrations
{
    public partial class RoleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesRoleId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f759524e-10e2-4ec8-86ed-b6013f2995bd", "35b37bee-3d4f-4b60-8eb9-29d46856696e", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "027631e0-6ea4-42b3-b3e9-2c72cf3c70d4", 0, "2d4a5588-96c7-47a3-bf3a-17700d60faac", "superadmin@example.com", true, "Super Administrator", true, new DateTime(2024, 9, 9, 23, 23, 9, 990, DateTimeKind.Local).AddTicks(5513), false, null, "SUPERADMIN@EXAMPLE.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEMo2EZgTbEuJab+L7xPz5PvKFMwZw7lvrUIMmqzz5MxI8MMTu8LiRfG4ID6HkuU2Jw==", null, false, "60ecbcf0-d9ca-4867-a90d-b642ca46d1cb", false, "superadmin" });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "Name" },
                values: new object[] { new Guid("ddc76fe0-5b4a-4d67-b829-428ae2dfb39d"), "SuperAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "f759524e-10e2-4ec8-86ed-b6013f2995bd", "027631e0-6ea4-42b3-b3e9-2c72cf3c70d4" });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f759524e-10e2-4ec8-86ed-b6013f2995bd", "027631e0-6ea4-42b3-b3e9-2c72cf3c70d4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f759524e-10e2-4ec8-86ed-b6013f2995bd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "027631e0-6ea4-42b3-b3e9-2c72cf3c70d4");
        }
    }
}
