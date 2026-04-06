using AdvertisingAgency.Model;
using AdvertisingAgency.Model.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Data
{
    public class AdvertisingDbContext:IdentityDbContext<User,Role,string>
    {
        private readonly IConfiguration configuration;

        public AdvertisingDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }


        public DbSet<AdvertisingCampaign> advertisingCampaigns { get; set; }
        public DbSet<CampaignCategory> campaignCategories { get; set; }
        public DbSet<CampaignStatus> campaignStatuses { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Model.Task> tasks { get; set; }
        public DbSet<Model.TaskStatus> taskStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql
                (configuration.GetConnectionString("PostgreSQLConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasIndex(e => e.name).IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasIndex(e => e.email).IsUnique();
                entity.HasOne(e => e.role)
                .WithMany(r => r.users)
                .HasForeignKey(e => e.roleId);
            });

            modelBuilder.Entity<AdvertisingCampaign>(entity =>
            {
                entity.ToTable("advertising_campaigns");
                entity.HasIndex(e => e.name).IsUnique();

                entity.HasOne(e => e.client)
                .WithMany(c => c.campaigns)
                .HasForeignKey(e => e.clientId);

                entity.HasOne(e => e.status)
                .WithMany(s => s.campaigns)
                .HasForeignKey(e => e.statusId);

                entity.HasOne(e => e.category)
                .WithMany(c => c.campaigns)
                .HasForeignKey(e => e.categoryId);
            });

            modelBuilder.Entity<CampaignCategory>(entity =>
            {
                entity.ToTable("campaign_categories");
                entity.HasIndex(e => e.name).IsUnique();
            });

            modelBuilder.Entity<CampaignStatus>(entity =>
            {
                entity.ToTable("campaig_statuses");
                entity.HasIndex(e => e.name).IsUnique();
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");
                entity.HasIndex(e => e.email).IsUnique();
                entity.HasIndex(e => e.phone).IsUnique();
                entity.Property(e => e.phone).HasMaxLength(11);
            });

            modelBuilder.Entity<Model.Task>(entity =>
            {
                entity.ToTable("tasks");
                entity.HasOne(e => e.campaign)
                .WithMany(c => c.tasks)
                .HasForeignKey(e => e.campaignId);
                entity.HasOne(e => e.assignedUser)
                .WithMany(u => u.tasks)
                .HasForeignKey(e => e.assignedUserId);
                entity.HasOne(e => e.taskStatus)
                .WithMany(ts => ts.tasks)
                .HasForeignKey(e => e.statusId);
            });

            modelBuilder.Entity<Model.TaskStatus>(entity =>
            {
                entity.ToTable("task_statuses");
                entity.HasIndex(e => e.name).IsUnique();
            });
        }
    }
}
