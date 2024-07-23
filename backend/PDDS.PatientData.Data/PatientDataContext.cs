using PDDS.PatientData.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace PDDS.PatientData.Data;

public partial class PatientDataContext : DbContext
{
    public PatientDataContext()
    {
    }

    public PatientDataContext(DbContextOptions<PatientDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");
            entity.HasKey(e => e.PatientID);

            entity.Property(e => e.PatientID)
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");

            entity.Property(e => e.DateOfBirth)
               .IsUnicode(false)
               .HasColumnName("DOB");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.UserID);

            entity.Property(e => e.UserID)
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
