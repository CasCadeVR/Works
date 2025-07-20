using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Works.Common;
using Works.Context;
using Works.Context.Contracts;
using Works.Export.Contracts;
using Works.Repository.Contracts.IReadRepositories;
using Works.Repository.Contracts.IWriteRepositories;
using Works.Repository.ReadRepositories;
using Works.Repository.WriteRepositories;
using Works.Services;
using Works.Services.Contracts;
using Works.Services.Contracts.IServices;
using Works.Services.Infrastructure;
using Works.Services.Services;
using Works.Web.Contracts.Infrastructure;

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
            builder.Services.AddScoped<IWorksServices, WorksServices>();

            builder.Services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            builder.Services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            builder.Services.AddScoped<ICustomerServices, CustomerService>();

            builder.Services.AddScoped<IExecutorReadRepository, ExecutorReadRepository>();
            builder.Services.AddScoped<IExecutorWriteRepository, ExecutorWriteRepository>();
            builder.Services.AddScoped<IExecutorServices, ExecutorServices>();

            builder.Services.AddScoped<IActReadRepository, ActReadRepository>();
            builder.Services.AddScoped<IActWriteRepository, ActWriteRepository>();
            builder.Services.AddScoped<IActServices, ActServices>();

            builder.Services.AddScoped<IActWorkWriteRepository, ActWorkWriteRepository>();

            builder.Services.AddScoped<IExporter, ExcelExporter>();

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