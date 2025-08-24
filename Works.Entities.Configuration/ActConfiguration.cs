using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CasCadeVR.Works.Entities.Configuration
{
    /// <summary>
    /// Описывает конфигурацию для <see cref="Act"/>
    /// </summary>
    public class ActConfiguration : IEntityTypeConfiguration<Act>
    {
        /// <summary>
        /// Конфигурация для <see cref="Act"/>
        /// </summary>
        public void Configure(EntityTypeBuilder<Act> builder)
        {
            builder.ToTable("Act");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ActNumber).IsRequired();
            builder.Property(x => x.NDS).IsRequired();
            builder.Property(x => x.Date).IsRequired();

            builder.HasIndex(x => x.ActNumber, $"IX_{nameof(Act)}_{nameof(Act.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Act.DeletedAt)}\" IS NULL");

            builder.HasOne(x => x.Executor)
                   .WithMany()
                   .HasForeignKey(x => x.ExecutorId);

            builder.HasOne(x => x.Customer)
                   .WithMany()
                   .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.Works)
                   .WithOne(x => x.Act)
                   .HasForeignKey(x => x.ActId);
        }
    }
}