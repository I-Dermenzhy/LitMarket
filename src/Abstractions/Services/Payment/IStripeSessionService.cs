using Domain.Models.Carts;

using Stripe.Checkout;

namespace Abstractions.Services.Payment;

public interface IStripeSessionService
{
    public Session CreateSession(IEnumerable<IBookCart> bookCarts, StripeSessionUrlSchemas schemas);
}

