using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientSocialDistance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userChangeDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VistDate",
                schema: "Hospital",
                table: "Vists",
                newName: "StartVistDate");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                schema: "Hospital",
                table: "Vists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsStartDateChange",
                schema: "Hospital",
                table: "Vists",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                schema: "Hospital",
                table: "Vists");

            migrationBuilder.DropColumn(
                name: "IsStartDateChange",
                schema: "Hospital",
                table: "Vists");

            migrationBuilder.RenameColumn(
                name: "StartVistDate",
                schema: "Hospital",
                table: "Vists",
                newName: "VistDate");
        }
    }
}
