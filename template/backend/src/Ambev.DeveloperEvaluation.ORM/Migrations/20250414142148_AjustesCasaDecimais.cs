using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AjustesCasaDecimais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "SaleItems",
                type: "numeric(13,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "SaleItems",
                type: "numeric(13,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "SaleItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "SaleItems",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(13,2)");
        }
    }
}
