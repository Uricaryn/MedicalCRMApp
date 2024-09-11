using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_CRM_Infrastructure.Migrations
{
    public partial class OperatorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureProduct_Procedures_ProcedureId",
                table: "ProcedureProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureProduct_Products_ProductId",
                table: "ProcedureProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_AspNetUsers_PerformedByUserId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_PerformedByUserId",
                table: "Procedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedureProduct",
                table: "ProcedureProduct");

            migrationBuilder.DropColumn(
                name: "PerformedByUserId",
                table: "Procedures");

            migrationBuilder.RenameTable(
                name: "ProcedureProduct",
                newName: "ProcedureProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ProcedureProduct_ProductId",
                table: "ProcedureProducts",
                newName: "IX_ProcedureProducts_ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "OperatorId",
                table: "Procedures",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PerformedByOperatorId",
                table: "Procedures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedureProducts",
                table: "ProcedureProducts",
                columns: new[] { "ProcedureId", "ProductId" });

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OperatorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_OperatorId",
                table: "Procedures",
                column: "OperatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_PerformedByOperatorId",
                table: "Procedures",
                column: "PerformedByOperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureProducts_Procedures_ProcedureId",
                table: "ProcedureProducts",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureProducts_Products_ProductId",
                table: "ProcedureProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_Operators_OperatorId",
                table: "Procedures",
                column: "OperatorId",
                principalTable: "Operators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_Operators_PerformedByOperatorId",
                table: "Procedures",
                column: "PerformedByOperatorId",
                principalTable: "Operators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureProducts_Procedures_ProcedureId",
                table: "ProcedureProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureProducts_Products_ProductId",
                table: "ProcedureProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_Operators_OperatorId",
                table: "Procedures");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_Operators_PerformedByOperatorId",
                table: "Procedures");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_OperatorId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_PerformedByOperatorId",
                table: "Procedures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedureProducts",
                table: "ProcedureProducts");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "PerformedByOperatorId",
                table: "Procedures");

            migrationBuilder.RenameTable(
                name: "ProcedureProducts",
                newName: "ProcedureProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ProcedureProducts_ProductId",
                table: "ProcedureProduct",
                newName: "IX_ProcedureProduct_ProductId");

            migrationBuilder.AddColumn<string>(
                name: "PerformedByUserId",
                table: "Procedures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedureProduct",
                table: "ProcedureProduct",
                columns: new[] { "ProcedureId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_PerformedByUserId",
                table: "Procedures",
                column: "PerformedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureProduct_Procedures_ProcedureId",
                table: "ProcedureProduct",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureProduct_Products_ProductId",
                table: "ProcedureProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_AspNetUsers_PerformedByUserId",
                table: "Procedures",
                column: "PerformedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
