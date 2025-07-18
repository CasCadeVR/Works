using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Works.Entities.Configuration
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

            builder.Property(x => x.INN).IsRequired();

            builder.Property(x => x.FIO)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Occupation)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Firm)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(x => x.FIO, $"IX_{nameof(Customer)}_{nameof(Customer.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Customer.DeletedAt)}\" IS NULL");

            builder.HasIndex(x => x.INN, $"IX_{nameof(Customer.INN)}_{nameof(Customer.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Customer.DeletedAt)}\" IS NULL");
        }
    }
}