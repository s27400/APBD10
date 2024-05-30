using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class PrescriptionEfConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(p => p.IdPrescription).HasName("IdPrescription");
        builder.Property(p => p.IdPrescription).UseIdentityColumn();

        builder.Property(p => p.Date).IsRequired();
        builder.Property(p => p.DueDate).IsRequired();

        builder.HasOne(p => p.DoctorNavigation)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdDoctor)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.PatientNavigation)
            .WithMany(p => p.Prescriptions)
            .HasForeignKey(p => p.IdPatient)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Prescription");

        Prescription[] prescriptions =
        {
            new Prescription()
            {
                IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Now.AddDays(10), IdDoctor = 1, IdPatient = 2
            },
            new Prescription()
            {
                IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Now.AddDays(15), IdDoctor = 1, IdPatient = 2
            },
            new Prescription()
            {
                IdPrescription = 3, Date = DateTime.Now, DueDate = DateTime.Now.AddDays(20), IdDoctor = 2, IdPatient = 1
            }
        };
        builder.HasData(prescriptions);
    }
}