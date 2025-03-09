using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixNameOfHangfireUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserH;angfires_Users_UserId",
                table: "UserH;angfires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserH;angfires",
                table: "UserH;angfires");

            migrationBuilder.RenameTable(
                name: "UserH;angfires",
                newName: "UserHangfires");

            migrationBuilder.RenameIndex(
                name: "IX_UserH;angfires_UserId",
                table: "UserHangfires",
                newName: "IX_UserHangfires_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserHangfires",
                table: "UserHangfires",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHangfires_Users_UserId",
                table: "UserHangfires",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHangfires_Users_UserId",
                table: "UserHangfires");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserHangfires",
                table: "UserHangfires");

            migrationBuilder.RenameTable(
                name: "UserHangfires",
                newName: "UserH;angfires");

            migrationBuilder.RenameIndex(
                name: "IX_UserHangfires_UserId",
                table: "UserH;angfires",
                newName: "IX_UserH;angfires_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserH;angfires",
                table: "UserH;angfires",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserH;angfires_Users_UserId",
                table: "UserH;angfires",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
