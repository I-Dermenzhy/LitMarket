using LitMarket.Domain.Models;

using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Users;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Customer : IdentityUser, IModel<string>
{
    public FullAddress FullAddress { get; set; }
}
