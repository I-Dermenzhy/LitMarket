using Domain.Models.Users;

namespace Abstractions.Repositories.Users;

public interface ICustomerRepository : IModelRepository<Customer, string>
{
}
