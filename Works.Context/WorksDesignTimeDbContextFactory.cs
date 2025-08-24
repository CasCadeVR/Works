using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CasCadeVR.Works.Context
{
    /// <summary>
    /// Создание контекста <see cref="WorksContext"/>
    /// </summary>
    public class WorksDesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorksContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context
        /// </summary>
        /// <remarks>
        /// 1) dotnet toWebApplication1ol install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet ef migrations add [name] --project Works.Context\Works.Context.csproj
        /// 4) dotnet ef database update --project Works.Context\Works.Context.csproj --connection "Host=localhost;Port=5432;Database=works;Username=postgres;Password=cCwatchM00N23234"
        /// 5) dotnet ef migrations update [targetMigrationName] --project Works.Context\Works.Context.csproj --connection "Host=localhost;Port=5432;Database=works;Username=postgres;Password=cCwatchM00N23234"
        /// 6) dotnet ef migrations remove --project Works.Context\Works.Context.csproj
        /// </remarks>
        public WorksContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<WorksContext>()
                .UseNpgsql()
                .LogTo(Console.WriteLine)
                .Options;

            return new WorksContext(options);
        }
    }
}

