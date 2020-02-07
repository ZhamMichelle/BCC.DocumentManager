using Microsoft.EntityFrameworkCore.Migrations;

namespace BCC.DocumentManager.Migrations
{
    public partial class rightNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstanceDocumentsdb_Filedb_FileId",
                table: "InstanceDocumentsdb");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceDocumentsdb_ProcessDocumentb_ProcessDocumentId",
                table: "InstanceDocumentsdb");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessDocumentb_Documentdb_DocumentId",
                table: "ProcessDocumentb");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessDocumentb_Processdb_ProcessId",
                table: "ProcessDocumentb");

            migrationBuilder.DropForeignKey(
                name: "FK_Viewdb_Processdb_ProcessId",
                table: "Viewdb");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewDocumentdb_Documentdb_DocumentId",
                table: "ViewDocumentdb");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewDocumentdb_Viewdb_ViewId",
                table: "ViewDocumentdb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ViewDocumentdb",
                table: "ViewDocumentdb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Viewdb",
                table: "Viewdb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessDocumentb",
                table: "ProcessDocumentb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processdb",
                table: "Processdb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstanceDocumentsdb",
                table: "InstanceDocumentsdb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documentdb",
                table: "Documentdb");

            migrationBuilder.RenameTable(
                name: "ViewDocumentdb",
                newName: "ViewDocuments");

            migrationBuilder.RenameTable(
                name: "Viewdb",
                newName: "Views");

            migrationBuilder.RenameTable(
                name: "ProcessDocumentb",
                newName: "ProcessDocuments");

            migrationBuilder.RenameTable(
                name: "Processdb",
                newName: "Processes");

            migrationBuilder.RenameTable(
                name: "InstanceDocumentsdb",
                newName: "InstanceDocuments");

            migrationBuilder.RenameTable(
                name: "Documentdb",
                newName: "Documents");

            migrationBuilder.RenameIndex(
                name: "IX_ViewDocumentdb_ViewId",
                table: "ViewDocuments",
                newName: "IX_ViewDocuments_ViewId");

            migrationBuilder.RenameIndex(
                name: "IX_ViewDocumentdb_DocumentId",
                table: "ViewDocuments",
                newName: "IX_ViewDocuments_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_Viewdb_ProcessId",
                table: "Views",
                newName: "IX_Views_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessDocumentb_ProcessId",
                table: "ProcessDocuments",
                newName: "IX_ProcessDocuments_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessDocumentb_DocumentId",
                table: "ProcessDocuments",
                newName: "IX_ProcessDocuments_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceDocumentsdb_ProcessDocumentId",
                table: "InstanceDocuments",
                newName: "IX_InstanceDocuments_ProcessDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceDocumentsdb_FileId",
                table: "InstanceDocuments",
                newName: "IX_InstanceDocuments_FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ViewDocuments",
                table: "ViewDocuments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Views",
                table: "Views",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessDocuments",
                table: "ProcessDocuments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processes",
                table: "Processes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstanceDocuments",
                table: "InstanceDocuments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceDocuments_Filedb_FileId",
                table: "InstanceDocuments",
                column: "FileId",
                principalTable: "Filedb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceDocuments_ProcessDocuments_ProcessDocumentId",
                table: "InstanceDocuments",
                column: "ProcessDocumentId",
                principalTable: "ProcessDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessDocuments_Documents_DocumentId",
                table: "ProcessDocuments",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessDocuments_Processes_ProcessId",
                table: "ProcessDocuments",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewDocuments_Documents_DocumentId",
                table: "ViewDocuments",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewDocuments_Views_ViewId",
                table: "ViewDocuments",
                column: "ViewId",
                principalTable: "Views",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Views_Processes_ProcessId",
                table: "Views",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstanceDocuments_Filedb_FileId",
                table: "InstanceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceDocuments_ProcessDocuments_ProcessDocumentId",
                table: "InstanceDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessDocuments_Documents_DocumentId",
                table: "ProcessDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessDocuments_Processes_ProcessId",
                table: "ProcessDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewDocuments_Documents_DocumentId",
                table: "ViewDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_ViewDocuments_Views_ViewId",
                table: "ViewDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Views_Processes_ProcessId",
                table: "Views");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Views",
                table: "Views");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ViewDocuments",
                table: "ViewDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Processes",
                table: "Processes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProcessDocuments",
                table: "ProcessDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstanceDocuments",
                table: "InstanceDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Views",
                newName: "Viewdb");

            migrationBuilder.RenameTable(
                name: "ViewDocuments",
                newName: "ViewDocumentdb");

            migrationBuilder.RenameTable(
                name: "Processes",
                newName: "Processdb");

            migrationBuilder.RenameTable(
                name: "ProcessDocuments",
                newName: "ProcessDocumentb");

            migrationBuilder.RenameTable(
                name: "InstanceDocuments",
                newName: "InstanceDocumentsdb");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Documentdb");

            migrationBuilder.RenameIndex(
                name: "IX_Views_ProcessId",
                table: "Viewdb",
                newName: "IX_Viewdb_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ViewDocuments_ViewId",
                table: "ViewDocumentdb",
                newName: "IX_ViewDocumentdb_ViewId");

            migrationBuilder.RenameIndex(
                name: "IX_ViewDocuments_DocumentId",
                table: "ViewDocumentdb",
                newName: "IX_ViewDocumentdb_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessDocuments_ProcessId",
                table: "ProcessDocumentb",
                newName: "IX_ProcessDocumentb_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessDocuments_DocumentId",
                table: "ProcessDocumentb",
                newName: "IX_ProcessDocumentb_DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceDocuments_ProcessDocumentId",
                table: "InstanceDocumentsdb",
                newName: "IX_InstanceDocumentsdb_ProcessDocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_InstanceDocuments_FileId",
                table: "InstanceDocumentsdb",
                newName: "IX_InstanceDocumentsdb_FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Viewdb",
                table: "Viewdb",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ViewDocumentdb",
                table: "ViewDocumentdb",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Processdb",
                table: "Processdb",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProcessDocumentb",
                table: "ProcessDocumentb",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstanceDocumentsdb",
                table: "InstanceDocumentsdb",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documentdb",
                table: "Documentdb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceDocumentsdb_Filedb_FileId",
                table: "InstanceDocumentsdb",
                column: "FileId",
                principalTable: "Filedb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceDocumentsdb_ProcessDocumentb_ProcessDocumentId",
                table: "InstanceDocumentsdb",
                column: "ProcessDocumentId",
                principalTable: "ProcessDocumentb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessDocumentb_Documentdb_DocumentId",
                table: "ProcessDocumentb",
                column: "DocumentId",
                principalTable: "Documentdb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessDocumentb_Processdb_ProcessId",
                table: "ProcessDocumentb",
                column: "ProcessId",
                principalTable: "Processdb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Viewdb_Processdb_ProcessId",
                table: "Viewdb",
                column: "ProcessId",
                principalTable: "Processdb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewDocumentdb_Documentdb_DocumentId",
                table: "ViewDocumentdb",
                column: "DocumentId",
                principalTable: "Documentdb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ViewDocumentdb_Viewdb_ViewId",
                table: "ViewDocumentdb",
                column: "ViewId",
                principalTable: "Viewdb",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
