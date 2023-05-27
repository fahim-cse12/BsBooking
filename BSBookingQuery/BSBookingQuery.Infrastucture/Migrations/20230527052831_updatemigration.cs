using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSBookingQuery.Infrastucture.Migrations
{
    /// <inheritdoc />
    public partial class updatemigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReplierName",
                table: "Replys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CommenterName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReplierName",
                table: "Replys");

            migrationBuilder.DropColumn(
                name: "CommenterName",
                table: "Comments");
        }
    }
}
