using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Works.Entities.Configuration
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
                .HasMaxLength(250);

            builder.Property(x => x.UnitOfMeasure)
                .HasMaxLength(50);

            builder.HasIndex(x => x.Name, $"IX_{nameof(Work)}_{nameof(Work.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Work.DeletedAt)}\" IS NULL");
        }
    }
}