using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoiaOnline.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoundRequests",
                columns: table => new
                {
                    TrackingNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsScraped = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundRequests", x => x.TrackingNumber);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoundRequests");
        }
    }
}
