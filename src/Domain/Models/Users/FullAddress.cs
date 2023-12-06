using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Users;

public class FullAddress
{
    [MaxLength(60)]
    public string? Country { get; set; }

    [MaxLength(60)]
    public string? City { get; set; }

    [MaxLength(60)]
    public string? StreetAddress { get; set; }

    [Length(6, 6)]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Postal code must be exactly 6 digits.")]
    public string? PostalCode { get; set; }
}