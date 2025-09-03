using Xunit;

namespace CasCadeVR.Works.Web.Tests.Infrastructures;

/// <summary>
/// Коллекция для интеграционных тестов
/// </summary>
[CollectionDefinition(nameof(WorksCollections))]
public class WorksCollections : ICollectionFixture<WorksApiFixture> { }