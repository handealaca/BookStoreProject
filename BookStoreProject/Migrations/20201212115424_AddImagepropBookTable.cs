using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreProject.Migrations
{
    public partial class AddImagepropBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagepath",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagepath",
                table: "Books");
        }
    }
}
