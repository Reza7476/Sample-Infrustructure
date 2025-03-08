using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class SetUserStatusInUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "UserStatus",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStatus",
                table: "Users");
        }
    }
}
