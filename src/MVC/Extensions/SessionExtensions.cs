using Stripe.Checkout;

namespace MVC.Extensions;

public static class SessionExtensions
{
    public static bool IsPaid(this Session session) => session.PaymentStatus.ToLower() is "paid";
}
