using Domain.Models.Users;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Orders;

#pragma warning disable CS8618
public class Shipping : IModel
{
    [Key]
    public int Id { get; set; }

    public DateOnly ArrivalDate { get; set; }

    [MaxLength(40)]
    public string? Carrier { get; set; }

    [Required]
    public string OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    [ValidateNever]
    public virtual Order Order { get; set; }

    [MaxLength(20)]
    public string? TrackingNumber { get; set; }

    [Required]
    public FullAddress ShippingAddress { get; set; }

    public DateTime ShippingDate { get; set; }
}
