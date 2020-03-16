using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BCC.DocumentManager.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOC_Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOC_FileContents",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_FileContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOC_Processes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOC_DocumentType",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_DocumentType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOC_DocumentType_DOC_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DOC_Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DOC_ProcessDocuments",
                columns: table => new
                {
                    ProcessId = table.Column<string>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    IsCapturePhoto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_ProcessDocuments", x => new { x.ProcessId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_DOC_ProcessDocuments_DOC_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DOC_Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DOC_ProcessDocuments_DOC_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "DOC_Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DOC_Views",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    ProcessId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOC_Views_DOC_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "DOC_Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DOC_Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BusinessKey = table.Column<string>(nullable: true),
                    ClientIin = table.Column<string>(nullable: true),
                    ColvirId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    FileContentId = table.Column<string>(nullable: true),
                    DocumentTypeId = table.Column<string>(nullable: true),
                    DocumentId = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    IsCapturePhoto = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOC_Files_DOC_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DOC_Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DOC_Files_DOC_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DOC_DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DOC_Files_DOC_FileContents_FileContentId",
                        column: x => x.FileContentId,
                        principalTable: "DOC_FileContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DOC_ViewDocuments",
                columns: table => new
                {
                    ViewId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false),
                    IsReadOnly = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOC_ViewDocuments", x => new { x.ViewId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_DOC_ViewDocuments_DOC_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "DOC_Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DOC_ViewDocuments_DOC_Views_ViewId",
                        column: x => x.ViewId,
                        principalTable: "DOC_Views",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DOC_DocumentType_DocumentId",
                table: "DOC_DocumentType",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_Files_BusinessKey",
                table: "DOC_Files",
                column: "BusinessKey");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_Files_ClientIin",
                table: "DOC_Files",
                column: "ClientIin");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_Files_ColvirId",
                table: "DOC_Files",
                column: "ColvirId");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_Files_DocumentId",
                table: "DOC_Files",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_Files_DocumentTypeId",
                table: "DOC_Files",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_Files_FileContentId",
                table: "DOC_Files",
                column: "FileContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DOC_ProcessDocuments_DocumentId",
                table: "DOC_ProcessDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_ViewDocuments_DocumentId",
                table: "DOC_ViewDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DOC_Views_ProcessId",
                table: "DOC_Views",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOC_Files");

            migrationBuilder.DropTable(
                name: "DOC_ProcessDocuments");

            migrationBuilder.DropTable(
                name: "DOC_ViewDocuments");

            migrationBuilder.DropTable(
                name: "DOC_DocumentType");

            migrationBuilder.DropTable(
                name: "DOC_FileContents");

            migrationBuilder.DropTable(
                name: "DOC_Views");

            migrationBuilder.DropTable(
                name: "DOC_Documents");

            migrationBuilder.DropTable(
                name: "DOC_Processes");
        }
    }
}
