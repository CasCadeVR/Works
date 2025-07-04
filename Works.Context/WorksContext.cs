using Works.Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Works.Context.Contracts;
using System.Diagnostics.CodeAnalysis;

namespace Works.Context
{
    /// <summary>
    /// контекст базы данных работы с работами
    /// </summary>
    public class WorksContext : DbContext, IReader, IWriter, IUnitOfWork
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

        /// <summary>
        /// 
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IEntityConfigurationAnchor).Assembly);
        }

        IQueryable<TEntity> IReader.Read<TEntity>()
           where TEntity : class
           => base.Set<TEntity>()
           .AsNoTracking()
           .AsQueryable();

        void IWriter.Add<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Added;

        void IWriter.Update<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Modified;

        void IWriter.Delete<TEntity>([NotNull] TEntity entity)
            => base.Entry(entity).State = EntityState.Deleted;

        async Task<int> IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken)
        {
            var count = await base.SaveChangesAsync(cancellationToken);
            foreach (var entry in base.ChangeTracker.Entries().ToArray())
            {
                entry.State = EntityState.Detached;
            }

            return count;
        }
    }
}