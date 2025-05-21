using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Works.Context
{
    /// <summary>
    /// 
    /// </summary>
    public class WorksDesignTimeDbContextFactory : IDesignTimeDbContextFactory<WorksContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context
        /// </summary>
        /// <remarks>
        /// 1) dotnet toWebApplication1ol install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet tool migrations add [name] --project Goods.Context\Goods.Context.csproj
        /// 4) dotnet tool database upgrade --project Goods.Context\Goods.Context.csproj
        /// 5) dotnet tool upgrade [targetMigrationName] --project Goods.Context\Goods.Context.csproj
        /// </remarks>
        public WorksContext CreateDbContext(string[] args)
        {
            var connectionString = "Host=localhost; Port=5432;Database=specular; Username=postgres; Password=Qwerty123456!";
            var options = new DbContextOptionsBuilder<WorksContext>()
                .UseNpgsql(connectionString)
                .LogTo(Console.WriteLine)
                .Options;

            return new WorksContext(options);
        }
    }
}

