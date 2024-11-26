using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothing.CMS.EntityFrameworkCore.Migrations
{
    public partial class Add_IsDeleted_Field_To_Role_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetRoles");
        }
    }
}
