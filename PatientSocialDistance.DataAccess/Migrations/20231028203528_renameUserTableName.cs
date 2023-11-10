using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientSocialDistance.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class renameUserTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_UserBlockedId",
                schema: "Hospital",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_AspNetUsers_UserMakeBlockId",
                schema: "Hospital",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_AspNetUsers_UserId",
                schema: "Hospital",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_AspNetUsers_VistorUserId",
                schema: "Hospital",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_AspNetUsers_UserId",
                schema: "Security",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_AspNetUsers_UserId",
                schema: "Security",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_AspNetUsers_UserId",
                schema: "Security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "Security",
                table: "UserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Vists_AspNetUsers_VistedUserId",
                schema: "Hospital",
                table: "Vists");

            migrationBuilder.DropForeignKey(
                name: "FK_Vists_AspNetUsers_VistorUserId",
                schema: "Hospital",
                table: "Vists");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Hospital");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "Security",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                schema: "Security",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                schema: "Security",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Security",
                table: "Users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "Security",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "Security",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hospital",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Security",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                schema: "Security",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApplicationUserId",
                schema: "Security",
                table: "Users",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                schema: "Security",
                table: "Users",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Users_UserBlockedId",
                schema: "Hospital",
                table: "Blocks",
                column: "UserBlockedId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Users_UserMakeBlockId",
                schema: "Hospital",
                table: "Blocks",
                column: "UserMakeBlockId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Users_UserId",
                schema: "Hospital",
                table: "Interactions",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_Users_VistorUserId",
                schema: "Hospital",
                table: "Interactions",
                column: "VistorUserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "Security",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "Security",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Security",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                schema: "Security",
                table: "Users",
                column: "UserTypeId",
                principalSchema: "Hospital",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ApplicationUserId",
                schema: "Security",
                table: "Users",
                column: "ApplicationUserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_Users_UserId",
                schema: "Security",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vists_Users_VistedUserId",
                schema: "Hospital",
                table: "Vists",
                column: "VistedUserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vists_Users_VistorUserId",
                schema: "Hospital",
                table: "Vists",
                column: "VistorUserId",
                principalSchema: "Security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Users_UserBlockedId",
                schema: "Hospital",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Users_UserMakeBlockId",
                schema: "Hospital",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Users_UserId",
                schema: "Hospital",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Interactions_Users_VistorUserId",
                schema: "Hospital",
                table: "Interactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_Users_UserId",
                schema: "Security",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_Users_UserId",
                schema: "Security",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                schema: "Security",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypeId",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ApplicationUserId",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_Users_UserId",
                schema: "Security",
                table: "UserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Vists_Users_VistedUserId",
                schema: "Hospital",
                table: "Vists");

            migrationBuilder.DropForeignKey(
                name: "FK_Vists_Users_VistorUserId",
                schema: "Hospital",
                table: "Vists");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ApplicationUserId",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypeId",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Age",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Hospital",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NationalId",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Nationality",
                schema: "Security",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                schema: "Security",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Security",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Hospital",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    Hospital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "Hospital",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalSchema: "Hospital",
                        principalTable: "UserTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Hospital",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ApplicationUserId",
                schema: "Hospital",
                table: "AspNetUsers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserTypeId",
                schema: "Hospital",
                table: "AspNetUsers",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Hospital",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_UserBlockedId",
                schema: "Hospital",
                table: "Blocks",
                column: "UserBlockedId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_AspNetUsers_UserMakeBlockId",
                schema: "Hospital",
                table: "Blocks",
                column: "UserMakeBlockId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_AspNetUsers_UserId",
                schema: "Hospital",
                table: "Interactions",
                column: "UserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Interactions_AspNetUsers_VistorUserId",
                schema: "Hospital",
                table: "Interactions",
                column: "VistorUserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_AspNetUsers_UserId",
                schema: "Security",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_AspNetUsers_UserId",
                schema: "Security",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_AspNetUsers_UserId",
                schema: "Security",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_AspNetUsers_UserId",
                schema: "Security",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vists_AspNetUsers_VistedUserId",
                schema: "Hospital",
                table: "Vists",
                column: "VistedUserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Vists_AspNetUsers_VistorUserId",
                schema: "Hospital",
                table: "Vists",
                column: "VistorUserId",
                principalSchema: "Hospital",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
