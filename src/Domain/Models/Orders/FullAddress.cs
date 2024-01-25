using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Users;

public class FullAddress
{
    [Length(2, 60)]
    public string? Country { get; set; }

    [Length(2, 60)]
    public string? City { get; set; }

    [Length(6, 6)]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Postal code must be exactly 6 digits.")]
    public string? PostalCode { get; set; }

    [Length(2, 60)]
    [MaxLength(60)]
    public string? StreetAddress { get; set; }

    public bool Equals(FullAddress fullAddress)
    {
        if (fullAddress is null)
            return false;

        if (Country != fullAddress.Country)
            return false;

        if (City != fullAddress.City)
            return false;

        if (PostalCode != fullAddress.PostalCode)
            return false;

        if (StreetAddress != fullAddress.StreetAddress)
            return false;

        return true;
    }
}