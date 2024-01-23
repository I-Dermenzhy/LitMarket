using Domain.Models.Users;

using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Orders;

#pragma warning disable CS8618
public class Shipping : IModel
{
    [Key]
    public int Id { get; set; }

    public DateOnly ArrivalDate { get; set; }

    [Length(3, 40)]
    public string? Carrier { get; set; }

    [Length(10, 30)]
    public string? TrackingNumber { get; set; }

    [Required]
    public FullAddress ShippingAddress { get; set; }

    public DateTime ShippingDate { get; set; }
}
