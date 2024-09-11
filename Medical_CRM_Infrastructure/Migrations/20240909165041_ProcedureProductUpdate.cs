using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_CRM_Infrastructure.Migrations
{
    public partial class ProcedureProductUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedureProducts",
                table: "ProcedureProducts");

            migrationBuilder.AddColumn<string>(
                name: "ProcedureProductId",
                table: "ProcedureProducts",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedureProducts",
                table: "ProcedureProducts",
                column: "ProcedureProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureProducts_ProcedureId",
                table: "ProcedureProducts",
                column: "ProcedureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcedureProducts",
                table: "ProcedureProducts");

            migrationBuilder.DropIndex(
                name: "IX_ProcedureProducts_ProcedureId",
                table: "ProcedureProducts");

            migrationBuilder.DropColumn(
                name: "ProcedureProductId",
                table: "ProcedureProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcedureProducts",
                table: "ProcedureProducts",
                columns: new[] { "ProcedureId", "ProductId" });
        }
    }
}
