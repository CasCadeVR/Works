using AutoMapper;
using CasCadeVR.Works.Services.Infrastructure;
using Xunit;

namespace CasCadeVR.Works.Services.Tests;

/// <summary>
/// Тесты для <see cref="ServiceProfile"/>
/// </summary>
public class AutoMapperProfileTests
{
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="AutoMapperProfileTests"/>
    /// </summary>
    public AutoMapperProfileTests()
    {
        var mapConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ServiceProfile>();
        });

        mapper = mapConfig.CreateMapper();
    }

    /// <summary>
    /// Тест на правильную настройку <see cref="ServiceProfile"/>
    /// </summary>
    [Fact]
    public void MapperConfigurationIsValid()
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
