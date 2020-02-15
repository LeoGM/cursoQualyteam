using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReceitasWebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receitas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    ImagemUrl = table.Column<string>(nullable: true),
                    Ingredientes = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    MetodoDePreparo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receitas");
        }
    }
}
