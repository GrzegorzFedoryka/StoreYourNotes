using Microsoft.EntityFrameworkCore.Migrations;

namespace storeYourNotes_webApi.Migrations
{
    public partial class PageRecordsTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageContents",
                table: "Pages");

            migrationBuilder.CreateTable(
                name: "PageRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreviousRecordId = table.Column<int>(type: "int", nullable: true),
                    NextRecordId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Contents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageRecords_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageRecords_PageId",
                table: "PageRecords",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageRecords");

            migrationBuilder.AddColumn<string>(
                name: "PageContents",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
