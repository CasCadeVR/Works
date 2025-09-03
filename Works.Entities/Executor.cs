using CasCadeVR.Works.Entities.Contracts;

namespace CasCadeVR.Works.Entities
{
    /// <summary>
    /// Сущность исполнителя
    /// </summary>
    public class Executor :  DataBaseEntity
    {
        /// <summary>
        /// ФИО  исполнителя
        /// </summary>
        public string FullName { get; set; } = string.Empty;

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
        public string RegistrationNumber { get; set; } = string.Empty;
    }
}