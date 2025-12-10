using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class _01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pacienti",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulpřed = table.Column<int>(type: "int", nullable: false),
                    Jmeno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prijmeni = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Titulza = table.Column<int>(type: "int", nullable: false),
                    DatumNarozeni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pohlaví = table.Column<int>(type: "int", nullable: false),
                    Cislo_pojistence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pojistovna = table.Column<int>(type: "int", nullable: false),
                    Doctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DatumVytvoreni = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacienti", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pacienti");
        }
    }
}
