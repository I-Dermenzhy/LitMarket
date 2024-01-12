using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Orders;

#pragma warning disable CS8618 
public class Payment : IModel
{
    [Key]
    public int Id { get; set; }

    public DateTime ApprovementDate { get; set; }
    public DateOnly DueDate { get; set; }

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
