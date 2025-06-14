using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<CourseAssignment> CourseAssignments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30);
            entity.HasMany(e => e.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10,2)");
            entity.HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId);
            entity.HasMany(c => c.CourseAssignments)
                .WithOne(ca => ca.Course)
                .HasForeignKey(ca => ca.CourseId);
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);
            entity.Property(e => e.EnrollDate)
                .IsRequired();
            entity.Property(e => e.Grade)
                .HasColumnType("decimal(3,2)");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.InstructorId);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30);
            entity.Property(e => e.LastName)
                .HasMaxLength(30);
            entity.Property(e => e.Phone)
                .HasMaxLength(20);
            entity.HasMany(i => i.CourseAssignments)
                .WithOne(ca => ca.Instructor)
                .HasForeignKey(ca => ca.InstructorId);
        });

        modelBuilder.Entity<CourseAssignment>(entity =>
        {
            entity.HasKey(e => e.CourseAssignmentId);
            entity.HasOne(ca => ca.Course)
                  .WithMany(c => c.CourseAssignments)
                  .HasForeignKey(ca => ca.CourseId);
            entity.HasOne(ca => ca.Instructor)
                  .WithMany(i => i.CourseAssignments)
                  .HasForeignKey(ca => ca.InstructorId);
        });
    }
}

