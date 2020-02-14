using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BCC.DocumentManager.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileId = table.Column<int>(nullable: false),
                    Data = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blobs_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false),
                    Required = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessDocuments_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    ProcessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Views_Processes_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstanceDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileId = table.Column<int>(nullable: false),
                    ProcessDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceDocuments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstanceDocuments_ProcessDocuments_ProcessDocumentId",
                        column: x => x.ProcessDocumentId,
                        principalTable: "ProcessDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ViewId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ViewDocuments_Views_ViewId",
                        column: x => x.ViewId,
                        principalTable: "Views",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blobs_FileId",
                table: "Blobs",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstanceDocuments_FileId",
                table: "InstanceDocuments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_InstanceDocuments_ProcessDocumentId",
                table: "InstanceDocuments",
                column: "ProcessDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDocuments_DocumentId",
                table: "ProcessDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessDocuments_ProcessId",
                table: "ProcessDocuments",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewDocuments_DocumentId",
                table: "ViewDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewDocuments_ViewId",
                table: "ViewDocuments",
                column: "ViewId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_ProcessId",
                table: "Views",
                column: "ProcessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blobs");

            migrationBuilder.DropTable(
                name: "InstanceDocuments");

            migrationBuilder.DropTable(
                name: "ViewDocuments");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "ProcessDocuments");

            migrationBuilder.DropTable(
                name: "Views");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Processes");
        }
    }
}
