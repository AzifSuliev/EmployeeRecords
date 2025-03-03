using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRecords.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Manager")]
        public int? ManagerId { get; set; } // Может быть null, если у отдела нет руководителя

        public virtual Employee? Manager { get; set; } // Навигационное свойство

        // Список сотрудников, работающих в этом отделе
        [NotMapped]
        public virtual List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
