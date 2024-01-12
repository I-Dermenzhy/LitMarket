using Abstractions.Repositories.Users;

using DataAccess.Data;

using Domain.Models.Users;

namespace DataAccess.Repositories.Users;

public class CustomerRepository(LitMarketDbContext db)
    : ModelRepository<Customer, string>(db), ICustomerRepository
{
}
