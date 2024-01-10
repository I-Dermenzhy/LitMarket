using Domain.Models.Users;

using LitMarket.Domain.Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Orders;

#pragma warning disable CS8618
public class Shipping : IModel
{
    [Key]
    public int Id { get; set; }

    [MaxLength(40)]
    public string? Carrier { get; set; }

    [Required]
    public FullAddress DeliveryAddress { get; set; }

    public DateTime DeliveryDate { get; set; }

    [Required]
    public string OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    [ValidateNever]
    public Order Order { get; set; }

    [MaxLength(20)]
    public string? TrackingNumber { get; set; }
}
