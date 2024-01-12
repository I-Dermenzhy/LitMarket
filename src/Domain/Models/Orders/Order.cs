using Domain.Models.Users;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Orders;

#pragma warning disable CS8618 
public class Order : IModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    [ValidateNever]
    public virtual Customer Customer { get; set; }

    [Required]
    public string ShippingId { get; set; }

    [ForeignKey(nameof(ShippingId))]
    [ValidateNever]
    public virtual Shipping Shipping { get; set; }

    public virtual IEnumerable<OrderItem> Items { get; set; }

    [Required]
    public int PaymentId { get; set; }

    [ForeignKey(nameof(PaymentId))]
    [ValidateNever]
    public virtual Payment Payment { get; set; }

    [Required]
    [Range(0, 100000)]
    public double TotalPrice { get; set; }

    [Required]
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Pending,
    Approved,
    Processing,
    Cancelled,
    Shipped,
    Delivered
}
