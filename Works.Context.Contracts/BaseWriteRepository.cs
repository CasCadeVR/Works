using System.Diagnostics.CodeAnalysis;
using CasCadeVR.Works.Common;
using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Context.Contracts;

/// <summary>
/// Базовый класс репозитория записи данных
/// </summary>
public abstract class BaseWriteRepository<T> : IDBWriter<T> where T : class
{
    private readonly IWriter writer;
    private readonly IDateTimeProvider dateTimeProvider;

    /// <summary>
    /// Инициализирует новый экземпляр <see cref="BaseWriteRepository{T}"/>
    /// </summary>
    protected BaseWriteRepository(IWriter writer, IDateTimeProvider dateTimeProvider)
    {
        this.writer = writer;
        this.dateTimeProvider = dateTimeProvider;
    }

    void IDBWriter<T>.Add([NotNull] T entity)
    {
        AuditCreate(entity);
        AuditUpdate(entity);
        writer.Add(entity);
    }

    void IDBWriter<T>.Update([NotNull] T entity)
    {
        AuditUpdate(entity);
        writer.Update(entity);
    }

    void IDBWriter<T>.Delete([NotNull] T entity)
    {
        if (entity is IEntitySoftDeleted softEntity)
        {
            AuditUpdate(entity);
            softEntity.DeletedAt = dateTimeProvider.UtcNow();
            writer.Update(entity);
        }
        else
        {
            writer.Delete(entity);
        }
    }

    private void AuditCreate([NotNull] T entity)
    {
        if (entity is IEntityWithAudit auditCreated)
        {
            auditCreated.CreatedAt = dateTimeProvider.UtcNow();
        }
    }

    private void AuditUpdate([NotNull] T entity)
    {
        if (entity is IEntityWithAudit auditCreated)
        {
            auditCreated.UpdatedAt = dateTimeProvider.UtcNow();
        }
    }
}