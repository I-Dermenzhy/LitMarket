using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Users;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Company : Customer
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}

