using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Works.Entities.Configuration
{
    /// <summary>
    /// Описывает конфигурацию для <see cref="ActWork"/>
    /// </summary>
    public class ActWorksConfiguration : IEntityTypeConfiguration<ActWork>
    {
        /// <summary>
        /// Конфигурация для <see cref="ActWork"/>
        /// </summary>
        public void Configure(EntityTypeBuilder<ActWork> builder)
        {
            builder.ToTable("ActWork");
            builder.HasKey(x => new { x.WorkId, x.ActId });

            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.Work)
                .WithMany()
                .HasForeignKey(x => x.WorkId);

            builder.HasOne(x => x.Act)
                .WithMany(x => x.Works)
                .HasForeignKey(x => x.ActId);
        }
    }
}