using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordyforestDotnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VocabulariesListVocabulary",
                columns: table => new
                {
                    VocabulariesId = table.Column<int>(type: "integer", nullable: false),
                    VocabulariesListsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocabulariesListVocabulary", x => new { x.VocabulariesId, x.VocabulariesListsId });
                    table.ForeignKey(
                        name: "FK_VocabulariesListVocabulary_VocabulariesLists_VocabulariesLi~",
                        column: x => x.VocabulariesListsId,
                        principalTable: "VocabulariesLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VocabulariesListVocabulary_Vocabularies_VocabulariesId",
                        column: x => x.VocabulariesId,
                        principalTable: "Vocabularies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocabulariesListVocabulary_VocabulariesListsId",
                table: "VocabulariesListVocabulary",
                column: "VocabulariesListsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VocabulariesListVocabulary");
        }
    }
}
