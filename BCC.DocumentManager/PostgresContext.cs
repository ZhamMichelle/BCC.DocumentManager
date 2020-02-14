using BCC.DocumentManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BCC.DocumentManager
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

        public PostgresContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
         //optionsBuilder.UseNpgsql("server=localhost; port=5432;UserId=postgres;Password=12345;database=postgres;");
        }

    }
}
