using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medical_CRM_Infrastructure.Migrations
{
    public partial class ConfigurationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureProducts_Procedures_ProcedureId",
                table: "ProcedureProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureProducts_Products_ProductId",
                table: "ProcedureProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureProducts_Procedures_ProcedureId",
                table: "ProcedureProducts",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureProducts_Products_ProductId",
                table: "ProcedureProducts",
                column: "ProductId",
                principalTable: "Products",
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
        }
    }
}
