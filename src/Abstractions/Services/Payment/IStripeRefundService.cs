using Stripe;

namespace Abstractions.Services.Payment;

public interface IStripeRefundService
{
    public Refund CreateRefund(string paymentIntentId, double total);
}
