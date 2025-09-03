using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CasCadeVR.Works.Common;
using CasCadeVR.Works.Context;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Export.Contracts;
using CasCadeVR.Works.Services;
using CasCadeVR.Works.Services.Contracts;
using CasCadeVR.Works.Services.Infrastructure;
using CasCadeVR.Works.Web.Infrastructure;
using CasCadeVR.Works.Common.Contracts;
using CasCadeVR.Works.Repository;
using CasCadeVR.Works.Export.Excel;

namespace CasCadeVR.Works.Web
{
    /// <summary>
    /// ¬ходна€ точка программы
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ¬ходной метод программы
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

            builder.Services.AddDbContext<WorksContext>(options =>
               options.UseNpgsql(connectionString)
               .LogTo(Console.WriteLine));

            builder.Services.AddScoped<IReader>(x => x.GetRequiredService<WorksContext>());
            builder.Services.AddScoped<IWriter>(x => x.GetRequiredService<WorksContext>());
            builder.Services.AddScoped<IUnitOfWork>(x => x.GetRequiredService<WorksContext>());
            
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

            builder.Services.AddRepositories();
            builder.Services.AddServices();

            builder.Services.AddScoped<IExporter, ExcelExporter>();
            builder.Services.AddScoped<IAddedTaxService, AddedTaxService>();

            var addedControllers = builder.Services.AddControllers(opt =>
            {
                opt.Filters.Add<WorksExceptionFilter>();
            });

            if (builder.Environment.EnvironmentName == "integration")
            {
                addedControllers.AddControllersAsServices();
            }

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Works.Web.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Works.Entities.xml"));
            });

            var app = builder.Build();

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