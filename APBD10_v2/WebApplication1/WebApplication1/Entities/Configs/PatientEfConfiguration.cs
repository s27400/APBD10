using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class PatientEfConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.IdPatient).HasName("IdPatient");
        builder.Property(p => p.IdPatient).UseIdentityColumn();
        
        builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Birthdate).IsRequired();
        
        builder.ToTable("Patient");

        Patient[] patients =
        {
            new Patient() { IdPatient = 1, FirstName = "Michal", LastName = "Michalowski", Birthdate = DateTime.Now },
            new Patient() { IdPatient = 2, FirstName = "Adam", LastName = "Adamowski", Birthdate = DateTime.Now },
            new Patient() { IdPatient = 3, FirstName = "Jan", LastName = "Janowski", Birthdate = DateTime.Now }
        };

        builder.HasData(patients);
    }
    
}