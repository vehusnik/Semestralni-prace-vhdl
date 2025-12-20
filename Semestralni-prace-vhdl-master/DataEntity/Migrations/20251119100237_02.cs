using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntity.Migrations
{
    /// <inheritdoc />
    public partial class _02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LekarskeZpravy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PacientId = table.Column<int>(type: "int", nullable: false),
                    Datumvysetreni = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Symptomy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnoza = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doporuceni = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LekarskeZpravy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LekarskeZpravy_Pacienti_PacientId",
                        column: x => x.PacientId,
                        principalTable: "Pacienti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LekarskeZpravy_PacientId",
                table: "LekarskeZpravy",
                column: "PacientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LekarskeZpravy");
        }
    }
}
