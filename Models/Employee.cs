using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeRecords.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Gender Gender { get; set; } // Используем enum вместо string

        public int PositionId { get; set; }
        public virtual Position? Position { get; set; } // Навигационное свойство

        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; } // Навигационное свойство

        [Required]
        public DateTime HireDate { get; set; }

        [Range(10000, 100000, ErrorMessage = "Зарплата должна быть в диапазоне от 10 000 до 100 000.")]
        public decimal Salary { get; set; }

        [EnumDataType(typeof(EmployeeStatus))]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EmployeeStatus EmployeeStatus { get; set; } = EmployeeStatus.Active;
    }

    public enum EmployeeStatus
    {
        Active,         // Активный сотрудник
        OnLeave,        // В отпуске
        SickLeave,      // На больничном
        MaternityLeave, // В декрете
        Suspended,      // Отстранен
        Terminated,     // Уволен
        Retired         // Вышел на пенсию
    }

    public enum Gender
    {
        Male,   // Мужской
        Female, // Женский
        Other   // Другой (или не указан)
    }
}
