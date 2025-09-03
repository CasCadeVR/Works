using CasCadeVR.Works.Entities.ValidationRules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasCadeVR.Works.Entities.Configuration
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
            builder.ToTable("Executor");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.RegistrationNumber)
                .IsRequired()
                .HasMaxLength(ExecutorValidationRules.RegistrationMaxLength);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(ExecutorValidationRules.FullNameMaxLength);

            builder.Property(x => x.Occupation)
                .IsRequired()
                .HasMaxLength(ExecutorValidationRules.OccupationMaxLength);

            builder.Property(x => x.Firm)
                .IsRequired()
                .HasMaxLength(ExecutorValidationRules.FirmMaxLength);

            builder.HasIndex(x => x.RegistrationNumber, $"IX_{nameof(Executor)}_{nameof(Executor.RegistrationNumber)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Executor.DeletedAt)}\" IS NULL");
        }
    }
}