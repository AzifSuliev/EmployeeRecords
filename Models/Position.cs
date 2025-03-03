using System;
using System.ComponentModel.DataAnnotations;

public class Position
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Title { get; set; }

    [Range(10000, 1000000, ErrorMessage = "Минимальная зарплата должна быть от 10 000 до 1 000 000.")]
    public decimal SalaryRangeMin { get; set; }

    [Range(10000, 1000000, ErrorMessage = "Максимальная зарплата должна быть от 10 000 до 1 000 000.")]
    public decimal SalaryRangeMax { get; set; }

    // Проверка, что Min <= Max
    public bool IsValidSalaryRange()
    {
        return SalaryRangeMin <= SalaryRangeMax;
    }
}
