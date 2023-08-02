using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedicineScheduler.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneratedColumnPriceTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PriceTotal",
                table: "Orders",
                type: "REAL",
                precision: 9,
                scale: 2,
                nullable: false,
                computedColumnSql: "QuantityPackages * PricePerPackage",
                stored: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldPrecision: 9,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PriceTotal",
                table: "Orders",
                type: "REAL",
                precision: 9,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldPrecision: 9,
                oldScale: 2,
                oldComputedColumnSql: "QuantityPackages * PricePerPackage");
        }
    }
}
