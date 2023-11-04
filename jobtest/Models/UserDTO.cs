using System.ComponentModel.DataAnnotations;

namespace jobtest.Models;

public class UserDTO
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(50)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;
}