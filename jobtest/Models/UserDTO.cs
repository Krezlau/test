using System.ComponentModel.DataAnnotations;

namespace jobtest.Models;

public class UserDTO
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = String.Empty;
    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
}