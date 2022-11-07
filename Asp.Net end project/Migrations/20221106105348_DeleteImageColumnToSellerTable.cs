using Microsoft.EntityFrameworkCore.Migrations;

namespace Asp.Net_end_project.Migrations
{
    public partial class DeleteImageColumnToSellerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Sellers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
