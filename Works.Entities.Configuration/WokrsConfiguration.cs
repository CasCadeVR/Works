using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Works.Entities.Configuration
{
    /// <summary>
    /// Описывает конфигурацию для <see cref="Work"/>
    /// </summary>
    public class WokrsConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.ToTable("Works");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);

            builder.HasIndex(x => x.Name, $"IX_{nameof(Work)}_{nameof(Work.DeletedAt)}")
                .IsUnique()
                .HasFilter($"\"{nameof(Work.DeletedAt)}\" IS NULL");
        }
    }
}