using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext()
        {
        }

        public HospitalDbContext(DbContextOptions option)
            :base(option)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<PatientMedicament> PatientsMedicaments { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Patient>(entity=>
            {
                entity.Property(p => p.FirstName)
                    .IsRequired(true)
                    .IsUnicode(true)
                    .HasMaxLength(50);
                entity.Property(p => p.LastName)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);
                entity.Property(p => p.Address)
                    .IsUnicode()
                    .HasMaxLength(250);
                entity.Property(p => p.Email)
                    .IsUnicode(false)
                    .HasMaxLength(80);
                entity.Property(p => p.HasInsurance)
                    .HasDefaultValue(true);
            });

            builder.Entity<Visitation>(entity =>
            {
                entity.HasKey(p => p.VisitationId);

                entity.Property(p => p.Date)
                    .HasColumnName("VisitationDate")
                    .IsRequired()
                    .HasColumnType("DATETIME2")
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(p => p.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(p => p.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(p => p.PatientId)
                    .HasConstraintName("FK_Visitation_Patient");

                entity.Property(p => p.DoctorId)
                    .IsRequired(false);

                entity.HasOne(p => p.Doctor)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(p => p.DoctorId);
            });

            builder.Entity<Diagnose>(entity =>
            {
                entity.HasKey(d => d.DiagnoseId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);
                entity.Property(p => p.Comments)
                    .IsRequired(false)
                    .IsUnicode()
                    .HasMaxLength(250);

                entity.HasOne(p => p.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(p => p.PatientId);
                    //.OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Medicament>(entity =>
            {
                entity.HasKey(p => p.MedicamentId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(50);
            });

            builder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(p => new { p.PatientId, p.MedicamentId });
                entity.HasOne(p => p.Medicament)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(p => p.MedicamentId)
                    .HasConstraintName("FK_Prescription_Medicament");

                entity.HasOne(p => p.Patient)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(p => p.PatientId)
                    .HasConstraintName("FK_Prescription_Patient");
            });

            builder.Entity<Doctor>(entity =>
            {
                entity.HasKey(d => d.DoctorId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);
                entity.Property(p => p.Specialty)
                    .IsRequired()
                    .IsUnicode()
                    .HasMaxLength(100);

            });
        }


    }
}
