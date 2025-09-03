using CasCadeVR.Works.Entities.ValidationRules;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasCadeVR.Works.Entities.Configuration
{
    /// <summary>
    /// Описывает конфигурацию для <see cref="Customer"/>
    /// </summary>
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Конфигурация для <see cref="Customer"/>
        /// </summary>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TaxPayerId)
                .IsRequired()
                .HasMaxLength(CustomerValidationRules.TaxPayerIdMaxLength);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(CustomerValidationRules.FullNameMaxLength);

            builder.Property(x => x.Occupation)
                .IsRequired()
                .HasMaxLength(CustomerValidationRules.OccupationMaxLength);

            builder.Property(x => x.Firm)
                .IsRequired()
                .HasMaxLength(CustomerValidationRules.FirmMaxLength);

            builder.HasIndex(x => x.TaxPayerId, $"IX_{nameof(Customer)}_{nameof(Customer.TaxPayerId)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Customer.DeletedAt)}\" IS NULL");
        }
    }
}