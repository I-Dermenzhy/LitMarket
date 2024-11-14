using Abstractions.Repositories;

using Microsoft.AspNetCore.Mvc;

using MVC.Extensions;

namespace MVC.ViewComponents;

public class ShoppingCartViewComponent(IUnitOfWork unitOfWork) : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

#pragma warning disable CS1998
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (this.GetUserClaim() is null)
            return View(0);

        if (HttpContext.Session.GetInt32(SessionValues.BookCartsCount) is null)
            UpdateCartCount();

        return View(HttpContext.Session.GetInt32(SessionValues.BookCartsCount));
    }

    private void UpdateCartCount()
    {
        int bookCartsCount = _unitOfWork.BookCarts.GetByCustomer(this.GetUserId()).Count();
        HttpContext.Session.SetInt32(SessionValues.BookCartsCount, bookCartsCount);
    }
}
