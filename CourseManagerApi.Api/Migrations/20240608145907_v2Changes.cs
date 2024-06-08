using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagerApi.Api.Migrations
{
    /// <inheritdoc />
    public partial class v2Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lead",
                type: "NVARCHAR(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Lead",
                type: "NVARCHAR(120)",
                maxLength: 120,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(120)",
                oldMaxLength: 120);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lead",
                type: "NVARCHAR(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Lead",
                type: "NVARCHAR(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR(120)",
                oldMaxLength: 120,
                oldNullable: true);
        }
    }
}
