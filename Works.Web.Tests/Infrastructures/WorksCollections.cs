using Xunit;

namespace Works.Web.Tests.Infrastructures;

/// <summary>
/// Коллекция для интеграционных тестов
/// </summary>
[CollectionDefinition(nameof(WorksCollections))]
public class WorksCollections : ICollectionFixture<WorksApiFixture>
{

}