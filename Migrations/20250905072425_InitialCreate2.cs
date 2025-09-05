using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace news.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage");

            migrationBuilder.DropIndex(
                name: "IX_NewsImage_infoNewsId",
                table: "NewsImage");

            migrationBuilder.DropColumn(
                name: "infoNewsId",
                table: "NewsImage");

            migrationBuilder.CreateIndex(
                name: "IX_NewsImage_NewsId",
                table: "NewsImage",
                column: "NewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_infoNews_NewsId",
                table: "NewsImage",
                column: "NewsId",
                principalTable: "infoNews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_infoNews_NewsId",
                table: "NewsImage");

            migrationBuilder.DropIndex(
                name: "IX_NewsImage_NewsId",
                table: "NewsImage");

            migrationBuilder.AddColumn<int>(
                name: "infoNewsId",
                table: "NewsImage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsImage_infoNewsId",
                table: "NewsImage",
                column: "infoNewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage",
                column: "infoNewsId",
                principalTable: "infoNews",
                principalColumn: "Id");
        }
    }
}
