using Works.Context.Contracts;

namespace Works.Entities
{
    /// <summary>
    /// Сущность работы
    /// </summary>
    public class Work : IEntityWithId, IEntityWithAudit, IEntitySoftDeleted
    {
        /// <inheritdoc cref="IEntityWithId.Id"/>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование работы
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание работы
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Цена работы за 1 единицу измерения
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc cref="IEntitySoftDeleted.DeletedAt"/>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}