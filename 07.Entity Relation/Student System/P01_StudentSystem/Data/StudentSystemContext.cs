using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_StudentSystem.Data.Models;


namespace P01_StudentSystem.Data
{
    public class StudentSystemContext : DbContext
    {

        public StudentSystemContext()
        {
        }
        public StudentSystemContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Homework> HomeworkSubmissions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if(!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Student>().HasKey(p => p.StudentId);

            builder.Entity<Student>(entity =>
            {
                entity.Property(n => n.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
                entity.Property(pn => pn.PhoneNumber)
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasColumnType("char(10)");
                entity.Property(b => b.Birthday)
                    .IsRequired(false);
            });

            builder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.CourseId);

                entity.Property(n => n.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(80);
                entity.Property(d => d.Description)
                    .IsRequired(false)
                    .IsUnicode();
            });

            builder.Entity<Resource>(entity =>
            {
                entity.HasKey(r => r.ResourceId);

                entity.Property(n => n.Name)
                    .IsUnicode()
                    .HasMaxLength(50);
                entity.Property(u => u.Url)
                    .IsUnicode(false);

                entity.HasOne(r => r.Course)
                    .WithMany(c => c.Resources)
                    .HasForeignKey(c => c.CourseId);
            });

            builder.Entity<Homework>(entity =>
            {
                entity.HasKey(h => h.HomeworkId);

                entity.Property(c => c.Content)
                    .IsUnicode(false);

                entity.HasOne(h => h.Student)
                    .WithMany(s => s.HomeworkSubmissions)
                    .HasForeignKey(s => s.StudentId);
                entity.HasOne(h => h.Course)
                    .WithMany(c => c.HomeworkSubmissions)
                    .HasForeignKey(c => c.CourseId);
            });

            builder.Entity<StudentCourse>(entity =>
            {
                entity.HasKey(sc => new { sc.StudentId, sc.CourseId });
                entity.HasOne(sc => sc.Student)
                    .WithMany(s => s.CourseEnrollments)
                    .HasForeignKey(s => s.StudentId);
                entity.HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentsEnrolled)
                    .HasForeignKey(c => c.CourseId);
            });
        }
    }
}
