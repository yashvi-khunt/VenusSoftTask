using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VenusSoftTask.Models;

namespace VenusSoftTask.Context;

public partial class PatientDBContext : DbContext
{
    public PatientDBContext()
    {
    }

    public PatientDBContext(DbContextOptions<PatientDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientEmail> PatientEmails { get; set; }

    public virtual DbSet<PatientPhoneNumber> PatientPhoneNumbers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=PatientDB;User=sa;Password=dockerStrongPwd123;Trusted_Connection=False;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3214EC076A3CFB65");

            entity.ToTable("Patient");

            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Attachment).IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Prefix)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PatientEmail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient___3214EC072A01539C");

            entity.ToTable("Patient_Email");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientEmails)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patient_E__Patie__412EB0B6");
        });

        modelBuilder.Entity<PatientPhoneNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient___3214EC07F10F321E");

            entity.ToTable("Patient_PhoneNumber");

            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientPhoneNumbers)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patient_P__Patie__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
