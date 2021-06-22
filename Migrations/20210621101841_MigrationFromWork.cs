using Microsoft.EntityFrameworkCore.Migrations;

namespace UserWallet.Migrations
{
    public partial class MigrationFromWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CuurencyCode",
                table: "Accounts",
                newName: "CuurrencyCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CuurrencyCode",
                table: "Accounts",
                newName: "CuurencyCode");
        }
    }
}
