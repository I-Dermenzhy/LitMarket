using Abstractions.Repositories.Orders;

using DataAccess.Data;

using Domain.Models.Orders;

namespace DataAccess.Repositories.Orders;
public class PaymentRepository(LitMarketDbContext db)
    : ModelRepository<Payment>(db), IPaymentRepository
{
    public void UpdateStatus(int id, PaymentStatus paymentStatus)
    {
        Payment payment = GetById(id);
        payment.Status = paymentStatus;
    }

    public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
    {
        Payment payment = GetById(id, isTracked: true);

        if (!string.IsNullOrEmpty(sessionId))
            payment.SessionId = sessionId;

        if (!string.IsNullOrEmpty(paymentIntentId))
        {
            payment.PaymentIntentId = paymentIntentId;
            payment.ApprovementDate = DateTime.Now;
        }
    }
}
