using Works.Entities.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Works.Context
{
    /// <summary>
    /// контекст базы данных работы с товарами
    /// </summary>
    public class WorksContext : DbContext
    {
        /// <summary>
        /// ctor
        /// </summary>
        public WorksContext(DbContextOptions<WorksContext> options) : base(options)
        {
            // https://support.aspnetzero.com/QA/Questions/11011/Cannot-write-DateTime-with-KindLocal-to-PostgreSQL-type-%27timestamp-with-time-zone%27-only-UTC-is-supported
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfigurationAnchor).Assembly);
        }
    }
}