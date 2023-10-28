using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdvertisementApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addDataToMilitaryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AdvertisementUserStatuses",
                columns: new[] { "Id", "Definition" },
                values: new object[,]
                {
                    { 1, "Done" },
                    { 2, "Deferred" },
                    { 3, "Exempt" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdvertisementUserStatuses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AdvertisementUserStatuses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AdvertisementUserStatuses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
