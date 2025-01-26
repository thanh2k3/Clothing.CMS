using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clothing.CMS.EntityFrameworkCore.Migrations
{
    public partial class Add_Price_To_OrderProduct_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "OrderProduct",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderProduct");
        }
    }
}
