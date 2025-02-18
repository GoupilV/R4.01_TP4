using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace R4._01_TP4.Migrations
{
    /// <inheritdoc />
    public partial class NewAnnotations1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "flm_genre",
                schema: "public",
                table: "t_e_film_flm",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<decimal>(
                name: "flm_duree",
                schema: "public",
                table: "t_e_film_flm",
                type: "numeric(3,0)",
                precision: 3,
                scale: 0,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,0)",
                oldPrecision: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "flm_genre",
                schema: "public",
                table: "t_e_film_flm",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "flm_duree",
                schema: "public",
                table: "t_e_film_flm",
                type: "numeric(3,0)",
                precision: 3,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,0)",
                oldPrecision: 3,
                oldScale: 0,
                oldNullable: true);
        }
    }
}
