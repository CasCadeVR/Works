using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Entities
{
    /// <summary>
    /// Сущность заказчика
    /// </summary>
    public class Customer : DataBaseEntity
    {
        /// <summary>
        /// ФИО заказчика
        /// </summary>
        public string FullName { get; set; } = string.Empty;

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
        public string TaxPayerId { get; set; } = string.Empty;
    }
}