using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BilgeAdamBlog.Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 20, nullable: true),
                    ModifiedUserId = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    Password = table.Column<string>(maxLength: 12, nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    LastIPAdress = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 20, nullable: true),
                    ModifiedUserId = table.Column<Guid>(nullable: true),
                    CategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 20, nullable: true),
                    ModifiedUserId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    PostDetail = table.Column<string>(nullable: false),
                    Tags = table.Column<string>(nullable: false),
                    ImagePath = table.Column<string>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_Users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CreatedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedIP = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedUserId = table.Column<Guid>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedComputerName = table.Column<string>(maxLength: 255, nullable: true),
                    ModifiedIP = table.Column<string>(maxLength: 20, nullable: true),
                    ModifiedUserId = table.Column<Guid>(nullable: true),
                    CommentText = table.Column<string>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Users_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Users_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedComputerName", "CreatedDate", "CreatedIP", "CreatedUserId", "Email", "FirstName", "ImageUrl", "LastIPAdress", "LastLogin", "LastName", "ModifiedComputerName", "ModifiedDate", "ModifiedIP", "ModifiedUserId", "Password", "Status", "Title" },
                values: new object[] { new Guid("82601534-af94-4e05-80d7-2898c31f4bd3"), null, null, null, null, "admin@admin.com", "Admin", "/", "127.0.0.1", new DateTime(2020, 11, 16, 19, 54, 53, 915, DateTimeKind.Local).AddTicks(8381), "Admin", null, null, null, null, "123", 1, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedUserId",
                table: "Categories",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedUserId",
                table: "Categories",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedUserId",
                table: "Comments",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ModifiedUserId",
                table: "Comments",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostId",
                table: "Comments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedUserId",
                table: "Posts",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ModifiedUserId",
                table: "Posts",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedUserId",
                table: "Users",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModifiedUserId",
                table: "Users",
                column: "ModifiedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
