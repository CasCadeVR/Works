using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Works.Entities.Configuration
{
    /// <summary>
    /// Описывает конфигурацию для <see cref="Executor"/>
    /// </summary>
    public class ExecutorConfiguration : IEntityTypeConfiguration<Executor>
    {
        /// <summary>
        /// Конфигурация для <see cref="Executor"/>
        /// </summary>
        public void Configure(EntityTypeBuilder<Executor> builder)
        {
            builder.ToTable("Executors");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OGRN).IsRequired();

            builder.Property(x => x.FIO)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Occupation)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Firm)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(x => x.FIO, $"IX_{nameof(Executor)}_{nameof(Executor.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Executor.DeletedAt)}\" IS NULL");

            builder.HasIndex(x => x.OGRN, $"IX_{nameof(Executor.OGRN)}_{nameof(Executor.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Executor.DeletedAt)}\" IS NULL");
        }
    }
}