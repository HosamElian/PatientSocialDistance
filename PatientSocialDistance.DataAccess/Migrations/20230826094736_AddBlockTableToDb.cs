using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientSocialDistance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserMakeBlockId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserBlockedId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlockedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blocks_AspNetUsers_UserBlockedId",
                        column: x => x.UserBlockedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Blocks_AspNetUsers_UserMakeBlockId",
                        column: x => x.UserMakeBlockId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_UserBlockedId",
                table: "Blocks",
                column: "UserBlockedId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_UserMakeBlockId",
                table: "Blocks",
                column: "UserMakeBlockId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blocks");
        }
    }
}
