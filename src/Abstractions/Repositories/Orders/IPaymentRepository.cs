using Domain.Models.Orders;

namespace Abstractions.Repositories.Orders;

public interface IPaymentRepository : IModelRepository<Payment>
{
    public void UpdateStatus(int id, PaymentStatus paymentStatus);
    public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId);
}
