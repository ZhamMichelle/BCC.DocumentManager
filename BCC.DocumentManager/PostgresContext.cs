using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BCC.DocumentManager.Models
{
    public class PostgresContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<ViewDocument> ViewDocuments { get; set; }
        public DbSet<ProcessDocument> ProcessDocuments { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<InstanceDocument> InstanceDocuments { get; set; }

        public PostgresContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         optionsBuilder.UseNpgsql("server=localhost; port=5432;UserId=postgres;Password=fuckdas26#l;database=postgres;");
        }

    }
}
