using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
	public partial class LikesAndDislikesOnSubComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubCommentsDislikes",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SubCommentId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCommentsDislikes", x => new { x.SubCommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SubCommentsDislikes_SubComments_SubCommentId",
                        column: x => x.SubCommentId,
                        principalTable: "SubComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCommentsDislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCommentsLikes",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    SubCommentId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCommentsLikes", x => new { x.SubCommentId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SubCommentsLikes_SubComments_SubCommentId",
                        column: x => x.SubCommentId,
                        principalTable: "SubComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubCommentsLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCommentsDislikes_UserId",
                table: "SubCommentsDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCommentsLikes_UserId",
                table: "SubCommentsLikes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCommentsDislikes");

            migrationBuilder.DropTable(
                name: "SubCommentsLikes");
        }
    }
}
