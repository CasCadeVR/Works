using Works.Context.Contracts;

namespace Works.Entities
{
    /// <summary>
    /// Сущность исполнителя
    /// </summary>
    public class Executor : IEntityWithId, IEntityWithAudit, IEntitySoftDeleted
    {
        /// <inheritdoc cref="IEntityWithId.Id"/>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО исполнителя
        /// </summary>
        public string FIO { get; set; } = string.Empty;

        /// <summary>
        /// Должность исполнителя
        /// </summary>
        public string Occupation { get; set; } = string.Empty;

        /// <summary>
        /// Фирма исполнителя
        /// </summary>
        public string Firm { get; set; } = string.Empty;

        /// <summary>
        /// ОГРН исполнителя
        /// </summary>
        public string OGRN { get; set; } = string.Empty;

        /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc cref="IEntitySoftDeleted.DeletedAt"/>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}