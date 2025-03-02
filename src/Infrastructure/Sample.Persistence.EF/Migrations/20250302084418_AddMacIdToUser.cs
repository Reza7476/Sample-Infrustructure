using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddMacIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MacId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MacId",
                table: "Users");
        }
    }
}
