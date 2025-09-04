using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace news.Migrations
{
    /// <inheritdoc />
    public partial class InfoNewsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage");

            migrationBuilder.AlterColumn<int>(
                name: "infoNewsId",
                table: "NewsImage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage",
                column: "infoNewsId",
                principalTable: "infoNews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage");

            migrationBuilder.AlterColumn<int>(
                name: "infoNewsId",
                table: "NewsImage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NewsImage_infoNews_infoNewsId",
                table: "NewsImage",
                column: "infoNewsId",
                principalTable: "infoNews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
