using Abstractions.Services.Payment;

using Domain.Models.Carts;

using Stripe.Checkout;

namespace Services.Payment;

public class StripeSessionService : IStripeSessionService
{
	public Session CreateSession(IEnumerable<IBookCart> bookCarts, StripeSessionUrlSchemas schemas)
	{
		var service = new SessionService();
		var options = CreateSessionOptions(bookCarts, schemas);

		return service.Create(options);
	}

	private static SessionCreateOptions CreateSessionOptions(IEnumerable<IBookCart> bookCarts, StripeSessionUrlSchemas schemas)
	{
		var options = new SessionCreateOptions
		{
			SuccessUrl = schemas.SuccessUrl,
			CancelUrl = schemas.CancelUrl,
			LineItems = CreateSessionLineItems(bookCarts),
			Mode = "payment",
		};

		return options;
	}

	private static List<SessionLineItemOptions> CreateSessionLineItems(IEnumerable<IBookCart> bookCarts)
	{
		List<SessionLineItemOptions> lineItems = [];

		foreach (var bookCart in bookCarts)
			lineItems.Add(CreateSessionLineItemOptions(bookCart));

		return lineItems;
	}

	private static SessionLineItemOptions CreateSessionLineItemOptions(IBookCart bookCart)
	{
		return new SessionLineItemOptions
		{
			PriceData = new SessionLineItemPriceDataOptions
			{
				// convert the amount to cents (default for Stripe)
				UnitAmount = (long)(bookCart.Book.PriceList.Price * 100),
				Currency = "usd",
				ProductData = new SessionLineItemPriceDataProductDataOptions
				{
					Name = bookCart.Book.Title
				}
			},
			Quantity = bookCart.Count
		};
	}
}
