using System.Reflection;
using FluentAssertions;
using Works.Web.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Goods.Web.Tests;

namespace Works.Web.Tests;

/// <summary>
/// 
/// </summary>
public class DependenciesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="DependenciesTests"/>
    /// </summary>
    public DependenciesTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestAppConfiguration();
            builder.UseEnvironment("integration");
        });
    }

    /// <summary>
    /// Проверяет связанность Dependecies в проекте
    /// </summary>
    [Theory]
    [MemberData(nameof(WebControllerCore))]
    public void ControllerCoreShouldBeResolved(Type controller)
    {
        // Arrange
        using var scope = factory.Services.CreateScope();

        // Act
        var instance = scope.ServiceProvider.GetRequiredService(controller);

        // Assert
        instance.Should().NotBeNull();
    }
    
    /// <summary>
    /// 
    /// </summary>
    public static TheoryData<Type> WebControllerCore => GetControllers<WorksController>();

    private static TheoryData<Type> GetControllers<TController>() =>
        new(Assembly.GetAssembly(typeof(TController))
            ?.DefinedTypes
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract));
}