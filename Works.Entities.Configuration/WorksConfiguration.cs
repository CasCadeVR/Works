using CasCadeVR.Works.Entities.ValidationRules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasCadeVR.Works.Entities.Configuration
{
    /// <summary>
    /// Описывает конфигурацию для <see cref="Work"/>
    /// </summary>
    public class WorksConfiguration : IEntityTypeConfiguration<Work>
    {
        /// <summary>
        /// Конфигурация для  <see cref="Work"/>
        /// </summary>
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Work");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(WorkValidationRules.NameMaxLength);

            builder.HasOne(x => x.UnitOfMeasure)
                .WithMany()
                .HasForeignKey(x => x.UnitOfMeasureId);

            builder.HasIndex(x => x.Name, $"IX_{nameof(Work)}_{nameof(Work.Name)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Work.DeletedAt)}\" IS NULL");
        }
    }
}