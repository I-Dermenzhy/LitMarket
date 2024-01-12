﻿using Domain.Models.Carts;

namespace Abstractions.Repositories.Carts;

public interface IBookCartRepository : IModelRepository<BookCart>
{
    public BookCart GetByCustomer(string customerId);
    public BookCart GetByCustomerAndBook(string customerId, int bookId);
}