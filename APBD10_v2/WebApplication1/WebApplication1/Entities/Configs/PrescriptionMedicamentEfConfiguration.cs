using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class PrescriptionMedicamentEfConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
{
    public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
    {
        builder.HasKey(pm => new { pm.IdMedication, pm.IdPrescription }).HasName("MedicationPrescription_pk");
        
        builder.HasOne(pm => pm.MedicamentNavigation)
            .WithMany(pm => pm.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedication)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pm => pm.PrescriptionNavigation)
            .WithMany(pm => pm.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(pm => pm.Details).HasMaxLength(100).IsRequired();

        builder.ToTable("Prescription_Medicament");

        PrescriptionMedicament[] prescriptionMedicaments =
        {
            new PrescriptionMedicament() { IdMedication = 1, IdPrescription = 1, Dose = 2, Details = "Details1" },
            new PrescriptionMedicament() { IdMedication = 2, IdPrescription = 2, Details = "Details2" },
            new PrescriptionMedicament() { IdMedication = 1, IdPrescription = 3, Dose = 3, Details = "Details3" }
        };

        builder.HasData(prescriptionMedicaments);
    }
}