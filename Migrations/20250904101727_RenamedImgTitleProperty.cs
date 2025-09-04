using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace news.Migrations
{
    /// <inheritdoc />
    public partial class RenamedImgTitleProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_infoNew_infoNewsId",
                table: "NewsImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_infoNew",
                table: "infoNew");

            migrationBuilder.RenameTable(
                name: "infoNew",
                newName: "infoNews");

            migrationBuilder.RenameColumn(
                name: "Img_Title",
                table: "infoNews",
                newName: "ImgTitle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_infoNews",
                table: "infoNews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage",
                column: "infoNewsId",
                principalTable: "infoNews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_infoNews",
                table: "infoNews");

            migrationBuilder.RenameTable(
                name: "infoNews",
                newName: "infoNew");

            migrationBuilder.RenameColumn(
                name: "ImgTitle",
                table: "infoNew",
                newName: "Img_Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_infoNew",
                table: "infoNew",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_infoNew_infoNewsId",
                table: "NewsImage",
                column: "infoNewsId",
                principalTable: "infoNew",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
