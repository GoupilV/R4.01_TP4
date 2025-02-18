using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R4._01_TP4.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDFilmRatinfs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "utl_datecreation",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "utl_datecreation",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
