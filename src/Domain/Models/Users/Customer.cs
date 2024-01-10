using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Users;

#pragma warning disable CS8618 
public class Customer : IdentityUser, IModel<string>
{
    public FullAddress FullAddress { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public CustomerType CustomerType { get; set; }
}

public enum CustomerType
{
    Individual,
    Company
}
