using System.ComponentModel.DataAnnotations;

namespace Topaz.Models;

public class Company
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Имя не указано")]
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Диапазон от 2 до 40")]
    public string? Name { get; set; }

    public List<User> Users { get; set; } = new List<User>();
}