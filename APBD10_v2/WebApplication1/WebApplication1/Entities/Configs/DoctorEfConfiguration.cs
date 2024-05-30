using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class DoctorEfConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.IdDoctor).HasName("IdDoctor");
        builder.Property(d => d.IdDoctor).UseIdentityColumn();

        builder.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(d => d.LastName).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Email).IsRequired().HasMaxLength(100);

        builder.ToTable("Doctor");

        Doctor[] doctors =
        {
            new Doctor() 
                { IdDoctor = 1, FirstName = "Adam", LastName = "Nowak", Email = "AdamNowak@wp.pl" },
            new Doctor()
                { IdDoctor = 2, FirstName = "Adrian", LastName = "Malinowski", Email = "AdrianMalinowski@onet.pl" },
            new Doctor()
                { IdDoctor = 3, FirstName = "Aleksandra", LastName = "Nowak", Email = "AleksandraNowak@gmail.com" }
        };

        builder.HasData(doctors);
    }
}