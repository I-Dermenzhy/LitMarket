using Abstractions.Repositories;

using DataAccess.Exceptions;

using Domain.Models.Carts;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MVC.Extensions;
using MVC.ViewModels;

using System.Diagnostics;

namespace MVC.Areas.Customer.Controllers;

[Area("Customer")]
internal sealed class HomeController(IUnitOfWork unitOfWork) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public IActionResult Index()
    {
        var books = _unitOfWork.Books.GetAll().OrderBy(b => b.Title);

        return View(books);
    }

    public IActionResult Details(int id)
    {
        BookCart bookCart = new()
        {
            Book = _unitOfWork.Books.GetById(id),
            BookId = id,
            Count = 1
        };

        return View(bookCart);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Details(BookCart bookCart)
    {
        bookCart.CustomerId = this.GetUserId();

        try
        {
            //shopping cart already exist, so we will be using it
            var cartFromBd = _unitOfWork.BookCarts.GetByCustomerAndBook(bookCart.CustomerId, bookCart.BookId);
            cartFromBd.Count += bookCart.Count;

            _unitOfWork.BookCarts.Update(cartFromBd);
            _unitOfWork.Save();
        }
        catch (ModelNotFoundException<BookCart>)
        {
            //no shopping cart exists for this user and book, so we add a new one
            bookCart.Id = 0;

            _unitOfWork.BookCarts.Add(bookCart);
            _unitOfWork.Save();

            UpdateCartCount(bookCart.CustomerId);
        }

        TempData["Success"] = "Books added to the cart successfully";

        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private void UpdateCartCount(string userId)
    {
        int bookCartsCount = _unitOfWork.BookCarts.GetByCustomer(userId).Count();
        HttpContext.Session.SetInt32("BookCartsCount", bookCartsCount);
    }
}
