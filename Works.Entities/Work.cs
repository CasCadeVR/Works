namespace Works.Entities
{
    /// <summary>
    /// Сущность работы
    /// </summary>
    public class Work
    {
        /// <summary>
        /// Идентификатор работы
        /// </summary>
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
        public int Price { get; set; } = 0;

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// Дата удалени записи
        /// </summary>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}