using Domain.Models.Users;

using LitMarket.Domain.Models;

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
    public Customer Customer { get; set; }

    public IEnumerable<OrderItem> Items { get; set; }

    [Required]
    public string PaymentId { get; set; }

    [ForeignKey(nameof(PaymentId))]
    [ValidateNever]
    public Payment Payment { get; set; }

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
