using Lab4.Model.Additional;
using Lab4.Model.Main;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data
{
    public class DeanDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DeanDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<ApplicationStatus> applicationStatuses { get; set; }
        public DbSet<ApplicationType> applicationTypes { get; set; }
        public DbSet<ControlForm> controlForms { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<SheetType> sheetTypes { get; set; }
        public DbSet<Spetialty> spetialties { get; set; }
        public DbSet<StudentStatus> studentStatuses { get; set; }
        public DbSet<TeacherPosition> teacherPositions { get; set; }


        public DbSet<AcademicRecord> academicRecords { get; set; }
        public DbSet<Application> applications { get; set; }
        public DbSet<Curriculum> curriculums { get; set; }
        public DbSet<DeaneryStaff> deaneryStaff { get; set; }
        public DbSet<Discipline> disciplines { get; set; }
        public DbSet<GradeSheet> gradeSheets { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Student> student { get; set; }
        public DbSet<Teacher> teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql
                (configuration.GetConnectionString("PostgreSQLConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationStatus>().ToTable("application_statuses");
            modelBuilder.Entity<ApplicationType>().ToTable("application_types");
            modelBuilder.Entity<ControlForm>().ToTable("control_forms");
            modelBuilder.Entity<Post>().ToTable("dean_posts");
            modelBuilder.Entity<SheetType>().ToTable("sheet_types");
            modelBuilder.Entity<Spetialty>().ToTable("specialties");
            modelBuilder.Entity<StudentStatus>().ToTable("student_statuses");
            modelBuilder.Entity<TeacherPosition>().ToTable("teacher_positions");


            modelBuilder.Entity<AcademicRecord>().ToTable("academic_records");
            modelBuilder.Entity<Application>().ToTable("applications");
            modelBuilder.Entity<Curriculum>().ToTable("curriculums");
            modelBuilder.Entity<DeaneryStaff>().ToTable("deanery_staff");
            modelBuilder.Entity<Discipline>().ToTable("disciplines");
            modelBuilder.Entity<GradeSheet>().ToTable("grade_sheets");
            modelBuilder.Entity<Group>().ToTable("groups");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<Teacher>().ToTable("teachers");

            modelBuilder.Entity<ApplicationStatus>()
            .HasIndex(a => a.name)
            .IsUnique();

            modelBuilder.Entity<ApplicationType>()
                .HasIndex(a => a.name)
                .IsUnique();

            modelBuilder.Entity<ControlForm>()
                .HasIndex(c => c.name)
                .IsUnique();

            modelBuilder.Entity<Post>()
                .HasIndex(d => d.name)
                .IsUnique();

            modelBuilder.Entity<SheetType>()
                .HasIndex(s => s.name)
                .IsUnique();

            modelBuilder.Entity<Spetialty>()
                .HasIndex(s => s.code)
                .IsUnique();

            modelBuilder.Entity<StudentStatus>()
                .HasIndex(s => s.name)
                .IsUnique();

            modelBuilder.Entity<TeacherPosition>()
                .HasIndex(t => t.name)
                .IsUnique();

            modelBuilder.Entity<Application>()
            .HasOne(a => a.order)
            .WithOne(o => o.application)
            .HasForeignKey<Order>(o => o.applicationId);

            modelBuilder.Entity<Curriculum>()
            .HasOne(c => c.group)
            .WithOne(g => g.curriculum)
            .HasForeignKey<Group>(g => g.curriculumId);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.phone).HasMaxLength(11);
                entity.Property(e => e.passportSerial).HasMaxLength(6);
                entity.Property(e => e.passportNumber).HasMaxLength(4);
            });

            modelBuilder.Entity<DeaneryStaff>(entity =>
            {
                entity.Property(e => e.phone).HasMaxLength(11);
                entity.Property(e => e.passportSerial).HasMaxLength(6);
                entity.Property(e => e.passportNumber).HasMaxLength(4);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.Property(e => e.phone).HasMaxLength(11);
                entity.Property(e => e.passportSerial).HasMaxLength(6);
                entity.Property(e => e.passportNumber).HasMaxLength(4);
            });
        }

    }
}
