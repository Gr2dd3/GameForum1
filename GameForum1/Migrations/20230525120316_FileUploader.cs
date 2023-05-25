using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameForum1.Migrations
{
    /// <inheritdoc />
    public partial class FileUploader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "File",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false)
                       .Annotation("SqlServer:Identity", "1, 1"),
                   FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                   UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_File", x => x.Id);
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
