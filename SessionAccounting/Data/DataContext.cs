using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SessionAccounting.Entities;

namespace SessionAccounting.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Faculty> Faculties => Set<Faculty>();

    public DbSet<Department> Departments => Set<Department>();

    public DbSet<StudentGroup> StudentGroups => Set<StudentGroup>();

    public DbSet<Teacher> Teachers => Set<Teacher>();

    public DbSet<Discipline> Disciplines => Set<Discipline>();

    public DbSet<Student> Students => Set<Student>();

    public DbSet<Session> Sessions => Set<Session>();

    public DbSet<SessionDiscipline> SessionDisciplines => Set<SessionDiscipline>();

    public DbSet<ScholarshipPayment> ScholarshipPayments => Set<ScholarshipPayment>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Не найдена строка подключения DefaultConnection.");

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureFaculty(modelBuilder);
        ConfigureDepartment(modelBuilder);
        ConfigureStudentGroup(modelBuilder);
        ConfigureTeacher(modelBuilder);
        ConfigureDiscipline(modelBuilder);
        ConfigureStudent(modelBuilder);
        ConfigureSession(modelBuilder);
        ConfigureSessionDiscipline(modelBuilder);
        ConfigureScholarshipPayment(modelBuilder);
    }

    private static void ConfigureFaculty(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Faculty>();
        entity.ToTable("Faculty");
        entity.HasKey(item => item.Id);
        entity.Property(item => item.Name).IsRequired().HasMaxLength(100);
        entity.Property(item => item.ShortName).IsRequired().HasMaxLength(20);
        entity.HasIndex(item => item.Name).IsUnique();
        entity.HasIndex(item => item.ShortName).IsUnique();
    }

    private static void ConfigureDepartment(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Department>();
        entity.ToTable("Department");
        entity.HasKey(item => item.Id);
        entity.Property(item => item.Name).IsRequired().HasMaxLength(150);
        entity.HasIndex(item => new { item.FacultyId, item.Name }).IsUnique();
        entity.HasOne<Faculty>()
            .WithMany()
            .HasForeignKey(item => item.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureStudentGroup(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<StudentGroup>();
        entity.ToTable("StudentGroup", table =>
            table.HasCheckConstraint("CK_StudentGroup_Course", "\"Course\" BETWEEN 1 AND 6"));
        entity.HasKey(item => item.Id);
        entity.Property(item => item.Number).IsRequired().HasMaxLength(20);
        entity.HasIndex(item => item.Number).IsUnique();
        entity.HasOne<Department>()
            .WithMany()
            .HasForeignKey(item => item.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureTeacher(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Teacher>();
        entity.ToTable("Teacher", table =>
            table.HasCheckConstraint("CK_Teacher_Position", "\"Position\" BETWEEN 0 AND 4"));
        entity.HasKey(item => item.Id);
        entity.Property(item => item.LastName).IsRequired().HasMaxLength(50);
        entity.Property(item => item.FirstName).IsRequired().HasMaxLength(50);
        entity.Property(item => item.MiddleName).HasMaxLength(50);
        entity.HasOne<Department>()
            .WithMany()
            .HasForeignKey(item => item.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureDiscipline(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Discipline>();
        entity.ToTable("Discipline", table =>
            table.HasCheckConstraint("CK_Discipline_Semester", "\"Semester\" IN (1, 2)"));
        entity.HasKey(item => item.Id);
        entity.Property(item => item.Name).IsRequired().HasMaxLength(150);
        entity.HasIndex(item => new { item.DepartmentId, item.Name, item.Semester }).IsUnique();
        entity.HasOne<Department>()
            .WithMany()
            .HasForeignKey(item => item.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureStudent(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Student>();
        entity.ToTable("Student", table =>
        {
            table.HasCheckConstraint("CK_Student_SocialStatus", "\"SocialStatus\" BETWEEN 0 AND 3");
            table.HasCheckConstraint("CK_Student_AverageGrade", "\"AverageGrade\" BETWEEN 2.00 AND 5.00");
            table.HasCheckConstraint("CK_Student_ScholarshipAmount", "\"ScholarshipAmount\" >= 0");
            table.HasCheckConstraint("CK_Student_DormitoryPayment", "\"DormitoryPayment\" >= 0");
        });
        entity.HasKey(item => item.Id);
        entity.Property(item => item.RecordBookNumber).IsRequired().HasMaxLength(20);
        entity.Property(item => item.LastName).IsRequired().HasMaxLength(50);
        entity.Property(item => item.FirstName).IsRequired().HasMaxLength(50);
        entity.Property(item => item.MiddleName).HasMaxLength(50);
        entity.Property(item => item.AverageGrade).HasPrecision(3, 2);
        entity.Property(item => item.ScholarshipAmount).HasPrecision(10, 2);
        entity.Property(item => item.DormitoryPayment).HasPrecision(10, 2);
        entity.HasIndex(item => item.RecordBookNumber).IsUnique();
        entity.HasOne<StudentGroup>()
            .WithMany()
            .HasForeignKey(item => item.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureSession(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Session>();
        entity.ToTable("Session");
        entity.HasKey(item => item.Id);
        entity.Property(item => item.SessionDate).HasColumnType("date");
        entity.HasIndex(item => new { item.StudentId, item.SessionDate }).IsUnique();
        entity.HasOne<Student>()
            .WithMany()
            .HasForeignKey(item => item.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureSessionDiscipline(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<SessionDiscipline>();
        entity.ToTable("SessionDiscipline", table =>
            table.HasCheckConstraint("CK_SessionDiscipline_Grade", "\"Grade\" BETWEEN 2 AND 5"));
        entity.HasKey(item => item.Id);
        entity.HasIndex(item => new { item.SessionId, item.DisciplineId }).IsUnique();
        entity.HasOne(item => item.Session)
            .WithMany(item => item.SessionDisciplines)
            .HasForeignKey(item => item.SessionId)
            .OnDelete(DeleteBehavior.Cascade);
        entity.HasOne(item => item.Discipline)
            .WithMany()
            .HasForeignKey(item => item.DisciplineId)
            .OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(item => item.Teacher)
            .WithMany()
            .HasForeignKey(item => item.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureScholarshipPayment(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<ScholarshipPayment>();
        entity.ToTable("ScholarshipPayment", table =>
            table.HasCheckConstraint("CK_ScholarshipPayment_Amount", "\"Amount\" >= 0"));
        entity.HasKey(item => item.Id);
        entity.Property(item => item.PaymentDate).HasColumnType("date");
        entity.Property(item => item.Amount).HasPrecision(10, 2);
        entity.HasOne<Student>()
            .WithMany()
            .HasForeignKey(item => item.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
