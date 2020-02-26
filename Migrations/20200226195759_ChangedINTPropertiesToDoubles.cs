using Microsoft.EntityFrameworkCore.Migrations;

namespace PartyThyme.Migrations
{
    public partial class ChangedINTPropertiesToDoubles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WaterNeeded",
                table: "Plants",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "LightNeeded",
                table: "Plants",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WaterNeeded",
                table: "Plants",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "LightNeeded",
                table: "Plants",
                type: "integer",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
