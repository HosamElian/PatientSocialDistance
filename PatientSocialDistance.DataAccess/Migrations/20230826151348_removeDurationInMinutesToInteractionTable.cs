using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientSocialDistance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class removeDurationInMinutesToInteractionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InteractionDurationInMinutes",
                table: "Interactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InteractionDurationInMinutes",
                table: "Interactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
