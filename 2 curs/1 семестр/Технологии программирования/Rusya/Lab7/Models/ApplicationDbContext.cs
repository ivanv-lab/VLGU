using Microsoft.EntityFrameworkCore;

namespace Lab7.Models
{
    public class ApplicationDbContext:DbContext
    {
        private readonly IConfiguration configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql
                (configuration.GetConnectionString("PostgreSQLConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Group>().ToTable("groups");
            modelBuilder.Entity<Contact>().ToTable("contacts");

            modelBuilder.Entity<Contact>()
                .HasOne(c => c.group)
                .WithMany(g => g.contacts)
                .HasForeignKey("group_id");
        }
    }
}
