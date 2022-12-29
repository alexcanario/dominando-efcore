using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DominandoEFCore.Migrations
{
    /// <inheritdoc />
    public partial class rg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rg",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rg",
                table: "Funcionarios");
        }
    }
}
