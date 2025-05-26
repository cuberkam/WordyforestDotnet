using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordyforestDotnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VocabulariesLists_Name_ExtendedUserId",
                table: "VocabulariesLists");

            migrationBuilder.DropIndex(
                name: "IX_SubscribedLists_ExtendedUserId_VocabulariesListId",
                table: "SubscribedLists");

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedLists_ExtendedUserId",
                table: "SubscribedLists",
                column: "ExtendedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubscribedLists_ExtendedUserId",
                table: "SubscribedLists");

            migrationBuilder.CreateIndex(
                name: "IX_VocabulariesLists_Name_ExtendedUserId",
                table: "VocabulariesLists",
                columns: new[] { "Name", "ExtendedUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscribedLists_ExtendedUserId_VocabulariesListId",
                table: "SubscribedLists",
                columns: new[] { "ExtendedUserId", "VocabulariesListId" },
                unique: true);
        }
    }
}
