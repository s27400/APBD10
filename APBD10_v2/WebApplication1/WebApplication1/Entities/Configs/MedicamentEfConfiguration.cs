using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Entities.Configs;

public class MedicamentEfConfiguration : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.HasKey(m => m.IdMedicament).HasName("IdMedicament");
        builder.Property(m => m.IdMedicament).UseIdentityColumn();

        builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Description).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Type).HasMaxLength(100).IsRequired();

        builder.ToTable("Medicament");

        Medicament[] medicaments =
        {
            new Medicament() { IdMedicament = 1, Name = "Apap", Description = "Desc1", Type = "Przeciwbolowy" },
            new Medicament() { IdMedicament = 2, Name = "Voltaren", Description = "Desc2", Type = "Masc" }
        };

        builder.HasData(medicaments);
    }
    
}