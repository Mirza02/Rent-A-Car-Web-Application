using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_A_Car_Quattro.Data.Migrations
{
    /// <inheritdoc />
    public partial class NovaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AktivneRezervacije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlijentId = table.Column<int>(type: "int", nullable: false),
                    RezervacijaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AktivneRezervacije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DostupnaVozila",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoziloId = table.Column<int>(type: "int", nullable: false),
                    PoslovnicaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DostupnaVozila", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Klijent",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Jmbg = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klijent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Poslovnica",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrojDostupnihVozila = table.Column<int>(type: "int", nullable: false),
                    BrojZaposlenika = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslovnica", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposlenik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Jmbg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uloga = table.Column<int>(type: "int", nullable: false),
                    ProvjeraStanjaVozila = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposlenik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LokacijaID = table.Column<int>(type: "int", nullable: true),
                    Kilometraza = table.Column<int>(type: "int", nullable: false),
                    CijenaPoSatu = table.Column<double>(type: "float", nullable: false),
                    VrijemeServisa = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BrojSasije = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transmisija = table.Column<int>(type: "int", nullable: false),
                    Gorivo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vozilo_Poslovnica_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Poslovnica",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoziloID = table.Column<int>(type: "int", nullable: true),
                    LokacijaID = table.Column<int>(type: "int", nullable: true),
                    KlijentID = table.Column<int>(type: "int", nullable: true),
                    PeriodRezervacije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CijenaRezervacije = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Klijent_KlijentID",
                        column: x => x.KlijentID,
                        principalTable: "Klijent",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Rezervacija_Poslovnica_LokacijaID",
                        column: x => x.LokacijaID,
                        principalTable: "Poslovnica",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Rezervacija_Vozilo_VoziloID",
                        column: x => x.VoziloID,
                        principalTable: "Vozilo",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KlijentID",
                table: "Rezervacija",
                column: "KlijentID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_LokacijaID",
                table: "Rezervacija",
                column: "LokacijaID");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_VoziloID",
                table: "Rezervacija",
                column: "VoziloID");

            migrationBuilder.CreateIndex(
                name: "IX_Vozilo_LokacijaID",
                table: "Vozilo",
                column: "LokacijaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AktivneRezervacije");

            migrationBuilder.DropTable(
                name: "DostupnaVozila");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Zaposlenik");

            migrationBuilder.DropTable(
                name: "Klijent");

            migrationBuilder.DropTable(
                name: "Vozilo");

            migrationBuilder.DropTable(
                name: "Poslovnica");
        }
    }
}
