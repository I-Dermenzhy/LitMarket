namespace Domain.Dto.Orders;

#pragma warning disable CS8618 
public class PaymentDto
{
    public int Id { get; set; }
    public DateTime ApprovementDate { get; set; }
    public DateOnly DueDate { get; set; }
    public string OrderId { get; set; }
    public string? PaymentIntentId { get; set; }
    public string Status { get; set; }
    public string? SessionId { get; set; }
}
