using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientSocialDistance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChangeAccepted",
                schema: "Hospital",
                table: "Notifications",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChangeDate",
                schema: "Hospital",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VisitId",
                schema: "Hospital",
                table: "Notifications",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeAccepted",
                schema: "Hospital",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsChangeDate",
                schema: "Hospital",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "VisitId",
                schema: "Hospital",
                table: "Notifications");
        }
    }
}
