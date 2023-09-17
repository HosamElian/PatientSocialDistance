using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientSocialDistance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationInMinutesToInteractionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Interactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Interactions");
        }
    }
}
