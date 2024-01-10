using LitMarket.Domain.Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Orders;

#pragma warning disable CS8618 
public class Payment : IModel
{
    [Key]
    public int Id { get; set; }

    public DateTime ApprovementDate { get; set; }
    public DateOnly DueDate { get; set; }

    [Required]
    public string OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    [ValidateNever]
    public Order Order { get; set; }

    [MaxLength(100)]
    public string? PaymentIntentId { get; set; }

    [MaxLength(40)]
    public PaymentStatus Status { get; set; }

    [MaxLength(100)]
    public string? SessionId { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Approved,
    Rejected,
    Cancelled,
    Refunded,
    Delayed
}
