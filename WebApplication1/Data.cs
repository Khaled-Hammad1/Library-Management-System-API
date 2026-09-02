using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectModels;
      Initial Catalog=project1;
      Integrated Security=True;
      TrustServerCertificate=True;");
        }
    }
}
