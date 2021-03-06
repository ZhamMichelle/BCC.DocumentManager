﻿// <auto-generated />
using Bcc.DocumentManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Bcc.DocumentManager.Migrations
{
    [DbContext(typeof(PostgresContext))]
    [Migration("20200217052242_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Bcc.DocumentManager.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClientIin")
                        .HasColumnType("text");

                    b.Property<string>("ColvirId")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DocumentId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.Process", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.ProcessDocument", b =>
                {
                    b.Property<string>("ProcessId")
                        .HasColumnType("text");

                    b.Property<int>("DocumentId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.HasKey("ProcessId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("ProcessDocuments");
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.View", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ProcessId")
                        .HasColumnType("integer");

                    b.Property<string>("ProcessId1")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProcessId1");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.ViewDocument", b =>
                {
                    b.Property<int>("ViewId")
                        .HasColumnType("integer");

                    b.Property<int>("DocumentId")
                        .HasColumnType("integer");

                    b.HasKey("ViewId", "DocumentId");

                    b.HasIndex("DocumentId");

                    b.ToTable("ViewDocuments");
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.File", b =>
                {
                    b.HasOne("Bcc.DocumentManager.Models.Document", "Document")
                        .WithMany("Files")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.ProcessDocument", b =>
                {
                    b.HasOne("Bcc.DocumentManager.Models.Document", "Document")
                        .WithMany("Processes")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bcc.DocumentManager.Models.Process", "Process")
                        .WithMany("Documents")
                        .HasForeignKey("ProcessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.View", b =>
                {
                    b.HasOne("Bcc.DocumentManager.Models.Process", "Process")
                        .WithMany("Views")
                        .HasForeignKey("ProcessId1");
                });

            modelBuilder.Entity("Bcc.DocumentManager.Models.ViewDocument", b =>
                {
                    b.HasOne("Bcc.DocumentManager.Models.Document", "Document")
                        .WithMany("Views")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bcc.DocumentManager.Models.View", "View")
                        .WithMany("Documents")
                        .HasForeignKey("ViewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
