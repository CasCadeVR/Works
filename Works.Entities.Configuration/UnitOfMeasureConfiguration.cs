using CasCadeVR.Works.Entities.ValidationRules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasCadeVR.Works.Entities.Configuration
{
    /// <summary>
    /// Описывает конфигурацию для <see cref="UnitOfMeasure"/>
    /// </summary>
    public class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
    {
        /// <summary>
        /// Конфигурация для  <see cref="UnitOfMeasure"/>
        /// </summary>
        public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
        {
            builder.ToTable("UnitOfMeasure");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(WorkValidationRules.NameMaxLength);

            builder.HasIndex(x => x.Name, $"IX_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.Name)}")
                .IsUnique()
                .HasFilter($"\"{nameof(UnitOfMeasure.DeletedAt)}\" IS NULL");
        }
    }
}