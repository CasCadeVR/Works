using System.Diagnostics.CodeAnalysis;
using CasCadeVR.Works.Context.Contracts;
using CasCadeVR.Works.Entities;
using CasCadeVR.Works.Repository.Contracts.IWriteRepositories;

namespace CasCadeVR.Works.Repository.WriteRepositories;

/// <inheritdoc cref="IActWorkWriteRepository"/>
public class ActWorkWriteRepository : IActWorkWriteRepository
{
    private readonly IWriter writer;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ActWorkWriteRepository"/>
    /// </summary>
    public ActWorkWriteRepository(IWriter writer)
    {
        this.writer = writer;
    }

    /// inheridoc<see cref="IWriter.Add{ActWork}(ActWork)"/>
    public void Add([NotNull] ActWork entity)
    {
        writer.Add(entity);
    }

    /// inheridoc<see cref="IWriter.Add{ActWork}(ActWork)"/>
    public void Update([NotNull] ActWork entity)
    {
        writer.Update(entity);
    }

    /// inheridoc<see cref="IWriter.Add{ActWork}(ActWork)"/>
    public void Delete([NotNull] ActWork entity)
    {
        writer.Delete(entity);
    }
}
