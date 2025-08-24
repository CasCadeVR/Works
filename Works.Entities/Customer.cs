using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Entities
{
    /// <summary>
    /// Сущность заказчика
    /// </summary>
    public class Customer : IEntityWithId, IEntityWithAudit, IEntitySoftDeleted
    {
        /// <inheritdoc cref="IEntityWithId.Id"/>
        public Guid Id { get; set; }

        /// <summary>
        /// ФИО заказчика
        /// </summary>
        public string FIO { get; set; } = string.Empty;

        /// <summary>
        /// Должность заказчика
        /// </summary>
        public string Occupation { get; set; } = string.Empty;

        /// <summary>
        /// Фирма заказчика
        /// </summary>
        public string Firm { get; set; } = string.Empty;

        /// <summary>
        /// ИНН Заказчика
        /// </summary>
        public string INN { get; set; } = string.Empty;

        /// <inheritdoc cref="IEntityWithAudit.CreatedAt"/>
        public DateTimeOffset CreatedAt { get; set; }

        /// <inheritdoc cref="IEntityWithAudit.UpdatedAt"/>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <inheritdoc cref="IEntitySoftDeleted.DeletedAt"/>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}