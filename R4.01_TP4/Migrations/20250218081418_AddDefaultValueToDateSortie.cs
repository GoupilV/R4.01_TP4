using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R4._01_TP4.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultValueToDateSortie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "utl_pays",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                defaultValue: "France",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "utl_datecreation",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "utl_cp",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "char(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "flm_datesortie",
                schema: "public",
                table: "t_e_film_flm",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_utilisateur_utl_utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl",
                column: "utl_mail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_t_e_utilisateur_utl_utl_mail",
                schema: "public",
                table: "t_e_utilisateur_utl");

            migrationBuilder.AlterColumn<string>(
                name: "utl_pays",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldDefaultValue: "France");

            migrationBuilder.AlterColumn<DateTime>(
                name: "utl_datecreation",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "utl_cp",
                schema: "public",
                table: "t_e_utilisateur_utl",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "flm_datesortie",
                schema: "public",
                table: "t_e_film_flm",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
