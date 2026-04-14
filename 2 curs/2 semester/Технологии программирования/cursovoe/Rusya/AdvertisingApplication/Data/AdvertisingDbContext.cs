using AdvertisingApplication.Model;
using AdvertisingApplication.Model.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskStatus = AdvertisingApplication.Model.TaskStatus;

namespace AdvertisingApplication.Data
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
        public DbSet<AdvertisingApplication.Model.Task> tasks { get; set; }
        public DbSet<TaskStatus> taskStatuses { get; set; }

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
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NormalizedName).HasColumnName("normalized_name");
                entity.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserName).HasColumnName("fullname");
                entity.Property(e => e.NormalizedUserName).HasColumnName("normalized_username");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.NormalizedEmail).HasColumnName("normalized_email");
                entity.Property(e => e.EmailConfirmed).HasColumnName("email_confirmed");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.SecurityStamp).HasColumnName("security_stamp");
                entity.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
                entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
                entity.Property(e => e.TwoFactorEnabled).HasColumnName("two_factor_enabled");
                entity.Property(e => e.LockoutEnabled).HasColumnName("lockout_enabled");
                entity.Property(e => e.LockoutEnd).HasColumnName("lockout_end");
                entity.Property(e => e.AccessFailedCount).HasColumnName("access_failed_count");
                entity.Property(e => e.roleId).HasColumnName("role_id");

                entity.HasOne(e => e.role)
                .WithMany(r => r.users)
                .HasForeignKey(e => e.roleId);

                entity.HasMany(e => e.tasks)
                .WithOne(e => e.assignedUser)
                .HasForeignKey(e => e.assignedUserId);
            });

            modelBuilder.Entity<AdvertisingCampaign>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");
                entity.Property(e => e.description).HasColumnName("description");
                entity.Property(e => e.startDate).HasColumnName("start_date");
                entity.Property(e => e.endDate).HasColumnName("end_date");
                entity.Property(e => e.budget).HasColumnName("budget");
                entity.Property(e => e.statusId).HasColumnName("status_id");
                entity.Property(e => e.categoryId).HasColumnName("category_id");
                entity.Property(e => e.clientId).HasColumnName("client_id");

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
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");

                entity.ToTable("campaign_categories");
                entity.HasIndex(e => e.name).IsUnique();
            });

            modelBuilder.Entity<CampaignStatus>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");

                entity.ToTable("campaign_statuses");
                entity.HasIndex(e => e.name).IsUnique();
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");
                entity.Property(e => e.contactPersonFullname).HasColumnName("contact_person_fullname");
                entity.Property(e => e.email).HasColumnName("email");
                entity.Property(e => e.phone).HasColumnName("phone");
                entity.Property(e => e.address).HasColumnName("address");

                entity.ToTable("clients");
                entity.HasIndex(e => e.name).IsUnique();
                entity.Property(e => e.phone).HasMaxLength(11);
            });

            modelBuilder.Entity<AdvertisingApplication.Model.Task>(entity =>
            {
                entity.ToTable("tasks");

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.title).HasColumnName("title");
                entity.Property(e => e.body).HasColumnName("body");
                entity.Property(e => e.deadline).HasColumnName("deadline");
                entity.Property(e => e.statusId).HasColumnName("status_id");
                entity.Property(e => e.assignedUserId).HasColumnName("assigned_user_id");
                entity.Property(e => e.campaignId).HasColumnName("campaign_id");

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

            modelBuilder.Entity<TaskStatus>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");

                entity.ToTable("task_statuses");
                entity.HasIndex(e => e.name).IsUnique();
            });
        }
    }
}
