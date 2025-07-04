using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Works.Common;
using Works.Context;
using Works.Context.Contracts;
using Works.Repository;
using Works.Repository.Contracts;
using Works.Services;
using Works.Services.Contracts;
using Works.Services.Infrastructure;
using Works.Web.Infrastructure;

namespace Works.Web
{
    /// <summary>
    /// ¬ходна€ точка программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ¬ходной метод программы
        /// </summary>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<WorksContext>(options =>
               options.UseNpgsql(connectionString)
               .LogTo(Console.WriteLine));

            builder.Services.AddScoped<IReader>(x => x.GetRequiredService<WorksContext>());
            builder.Services.AddScoped<IWriter>(x => x.GetRequiredService<WorksContext>());
            builder.Services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<WorksContext>());
            
            builder.Services.AddScoped<IWorksServices, WorksServices>();
            builder.Services.AddSingleton<IValidateService, ValidateService>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            builder.Services.AddSingleton(_ =>
            {
                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<ServiceProfile>();
                    cfg.AddProfile<ApiMapper>();
                });

                var mapper = mapConfig.CreateMapper();
                return mapper;
            });

            builder.Services.AddScoped<IWorksReadRepository, WorksReadRepository>();
            builder.Services.AddScoped<IWorksWriteRepository, WorksWriteRepository>();

            var addedControllers = builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add<WorksExceptionFilter>();
            });

            if (builder.Environment.EnvironmentName == "integration")
            {
                addedControllers.AddControllersAsServices();
            }

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Works.Web.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Works.Entities.xml"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}