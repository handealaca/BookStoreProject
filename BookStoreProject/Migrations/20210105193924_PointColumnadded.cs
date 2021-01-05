using Microsoft.EntityFrameworkCore.Migrations;


namespace BookStoreProject.Migrations
{
    public partial class PointColumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "UserPoints",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "UserPoints");
        }
    }
}
