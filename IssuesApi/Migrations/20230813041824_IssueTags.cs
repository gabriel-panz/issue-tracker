using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IssuesApi.Migrations
{
    /// <inheritdoc />
    public partial class IssueTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 13, 1, 18, 23, 969, DateTimeKind.Local).AddTicks(8585),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 7, 13, 19, 56, 20, 265, DateTimeKind.Local).AddTicks(8167));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 8, 13, 1, 18, 23, 968, DateTimeKind.Local).AddTicks(9067),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 7, 13, 19, 56, 20, 265, DateTimeKind.Local).AddTicks(2502));

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2023, 8, 13, 1, 18, 23, 970, DateTimeKind.Local).AddTicks(1912)),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IssueTag",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagId = table.Column<long>(type: "INTEGER", nullable: false),
                    IssueId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueTag_IssueItem_IssueId",
                        column: x => x.IssueId,
                        principalTable: "IssueItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IssueTag_IssueId",
                table: "IssueTag",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueTag_TagId",
                table: "IssueTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_Id",
                table: "Tag",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Project",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 13, 19, 56, 20, 265, DateTimeKind.Local).AddTicks(8167),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 8, 13, 1, 18, 23, 969, DateTimeKind.Local).AddTicks(8585));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "IssueItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(2023, 7, 13, 19, 56, 20, 265, DateTimeKind.Local).AddTicks(2502),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldDefaultValue: new DateTime(2023, 8, 13, 1, 18, 23, 968, DateTimeKind.Local).AddTicks(9067));
        }
    }
}
