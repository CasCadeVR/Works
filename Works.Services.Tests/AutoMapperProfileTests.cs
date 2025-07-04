using AutoMapper;
using Works.Services.Infrastructure;
using Xunit;

namespace Works.Services.Tests;

/// <summary>
/// Тесты для <see cref="ServiceProfile"/>
/// </summary>
public class AutoMapperProfileTests
{
    private readonly IMapper mapper;

    /// <summary>
    /// ctor
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
    /// Тест на пустую ошибку наименования
    /// </summary>
    [Fact]
    public void ValideMapperConfiguration()
    {
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}
