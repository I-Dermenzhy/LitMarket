using LitMarket.Domain.Models;

using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Users;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Customer : IdentityUser, IModel<string>
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    public FullAddress FullAddress { get; set; }
}
