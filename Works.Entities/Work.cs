using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Entities
{
    /// <summary>
    /// Сущность работы
    /// </summary>
    public class Work : DataBaseEntity
    {
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

        /// <summary>
        /// Идентификатор <see cref="UnitOfMeasure"/>
        /// </summary>
        public Guid UnitOfMeasureId { get; set; }

        /// <summary>
        /// Навигационное свойство <see cref="UnitOfMeasure"/>
        /// </summary>
        public UnitOfMeasure UnitOfMeasure { get; set; } = null!;
    }
}