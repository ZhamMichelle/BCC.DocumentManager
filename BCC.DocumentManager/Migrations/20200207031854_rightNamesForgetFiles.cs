using Microsoft.EntityFrameworkCore.Migrations;

namespace BCC.DocumentManager.Migrations
{
    public partial class rightNamesForgetFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstanceDocuments_Filedb_FileId",
                table: "InstanceDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Filedb",
                table: "Filedb");

            migrationBuilder.RenameTable(
                name: "Filedb",
                newName: "Files");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceDocuments_Files_FileId",
                table: "InstanceDocuments",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstanceDocuments_Files_FileId",
                table: "InstanceDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "Filedb");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Filedb",
                table: "Filedb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceDocuments_Filedb_FileId",
                table: "InstanceDocuments",
                column: "FileId",
                principalTable: "Filedb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
