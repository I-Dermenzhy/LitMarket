using Abstractions.Services.Payment;

using Stripe;

namespace Services.Payment;

public class StripeRefundService : IStripeRefundService
{
    public Refund CreateRefund(string paymentIntentId, double total)
    {
        var options = CreateRefundOptions(paymentIntentId, total);
        var service = new RefundService();

        return service.Create(options);
    }

    private static RefundCreateOptions CreateRefundOptions(string paymentIntentId, double total)
    {
        return new RefundCreateOptions
        {
            // convert the amount to cents (default for Stripe)
            Amount = Convert.ToInt32(total * 100),
            Reason = RefundReasons.RequestedByCustomer,
            PaymentIntent = paymentIntentId
        };
    }
}
