﻿using Bcc.DocumentManager.Models;
using Microsoft.EntityFrameworkCore;

namespace Bcc.DocumentManager
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

        public DbSet<Document> Documents { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<ViewDocument> ViewDocuments { get; set; }
        public DbSet<ProcessDocument> ProcessDocuments { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<FileContent> FileContents { get; set; }

        public PostgresContext()
        {
            Database.EnsureCreated();
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcessDocument>()
                .HasKey(bc => new { bc.ProcessId, bc.DocumentId });
            modelBuilder.Entity<ProcessDocument>()
                .HasOne(bc => bc.Process)
                .WithMany(b => b.Documents)
                .HasForeignKey(bc => bc.ProcessId);
            modelBuilder.Entity<ProcessDocument>()
                .HasOne(bc => bc.Document)
                .WithMany(c => c.Processes)
                .HasForeignKey(bc => bc.DocumentId);

            modelBuilder.Entity<ViewDocument>()
                .HasKey(bc => new { bc.ViewId, bc.DocumentId });
            modelBuilder.Entity<ViewDocument>()
                .HasOne(bc => bc.View)
                .WithMany(b => b.Documents)
                .HasForeignKey(bc => bc.ViewId);
            modelBuilder.Entity<ViewDocument>()
                .HasOne(bc => bc.Document)
                .WithMany(c => c.Views)
                .HasForeignKey(bc => bc.DocumentId);

            modelBuilder.Entity<File>()
                .HasIndex(f => f.BusinessKey);
            modelBuilder.Entity<File>()
                .HasIndex(f => f.ClientIin);
            modelBuilder.Entity<File>()
                .HasIndex(f => f.ColvirId);
            
            modelBuilder.Entity<File>()
                .HasOne(f => f.FileContent)
                .WithOne(fc => fc.File)
                .OnDelete(DeleteBehavior.SetNull);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName($"DOC_{entity.GetTableName()}");
            }
        }

    }
}
