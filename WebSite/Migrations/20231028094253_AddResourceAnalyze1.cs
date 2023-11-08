using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSite.Migrations
{
    /// <inheritdoc />
    public partial class AddResourceAnalyze1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegexFix",
                table: "ResourcesAnalyze",
                newName: "RegexReplacement");

            migrationBuilder.AddColumn<string>(
                name: "RegexPattern",
                table: "ResourcesAnalyze",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegexPattern",
                table: "ResourcesAnalyze");

            migrationBuilder.RenameColumn(
                name: "RegexReplacement",
                table: "ResourcesAnalyze",
                newName: "RegexFix");
        }
    }
}
