using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LotionCream.API.Migrations
{
    public partial class colorAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareType");

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ColorID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.CreateTable(
                name: "ShareType",
                columns: table => new
                {
                    ShareTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommentID = table.Column<int>(nullable: true),
                    PostID = table.Column<long>(nullable: true),
                    ReplyID = table.Column<int>(nullable: true),
                    ShareTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareType", x => x.ShareTypeID);
                    table.ForeignKey(
                        name: "FK_ShareType_Comments_CommentID",
                        column: x => x.CommentID,
                        principalTable: "Comments",
                        principalColumn: "CommentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShareType_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShareType_Replies_ReplyID",
                        column: x => x.ReplyID,
                        principalTable: "Replies",
                        principalColumn: "ReplyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShareType_CommentID",
                table: "ShareType",
                column: "CommentID");

            migrationBuilder.CreateIndex(
                name: "IX_ShareType_PostID",
                table: "ShareType",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_ShareType_ReplyID",
                table: "ShareType",
                column: "ReplyID");
        }
    }
}
