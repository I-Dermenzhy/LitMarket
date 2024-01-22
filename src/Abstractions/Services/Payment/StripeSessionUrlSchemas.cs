namespace Abstractions.Services.Payment;

public class StripeSessionUrlSchemas
{
    public required string SuccessUrl { get; set; }
    public required string CancelUrl { get; set; }
}
