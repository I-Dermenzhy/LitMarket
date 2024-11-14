using Abstractions.Repositories;

using AutoMapper;

using DataAccess.Exceptions;

using Domain.Dto.Books;
using Domain.Models.Books;
using Domain.Models.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using MVC.Areas.Admin.ViewModels;
using MVC.Services;

namespace MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public sealed class BookController(
    IUnitOfWork unitOfWork,
    IFileHelper fileHelper,
    IMapper mapper) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IFileHelper _fileHelper = fileHelper;
    private readonly IMapper _mapper = mapper;

    public IActionResult Index()
    {
        var books = _unitOfWork.Books.GetAll();

        return View(books);
    }

    public IActionResult Upsert(int? id)
    {
        BookVM bookVM = new()
        {
            Genres = GetExistingGenresAsSelectListItems(),
            Book = new Book()
        };

        //for the update scenario 
        if (id is not null and not 0)
            bookVM.Book = _unitOfWork.Books.GetById((int)id);

        return View(bookVM);
    }

    [HttpPost]
    public IActionResult Upsert(BookVM bookVM)
    {
        var book = bookVM.Book;

        if (!ModelState.IsValid)
            return RedirectToSamePage(bookVM);

        if (bookVM.Book.Id == 0)
        {
            _unitOfWork.Books.Add(book);
            TempData["Success"] = "Book created successfully";
        }
        else
        {
            _unitOfWork.Books.Update(book);
            TempData["Success"] = "Book updated successfully";
        }

        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult AddImage(IFormFile file, BookImageType imageType, int bookId)
    {
        Book book = _unitOfWork.Books.GetById(bookId);

        _fileHelper.DownloadBookImage(file, book.Title, out string imageUrl);

        BookImage bookImage = new()
        {
            BookId = book.Id,
            Url = imageUrl,
            ImageType = imageType
        };

        book.Images ??= new List<BookImage>();

        book.Images.Add(bookImage);
        _unitOfWork.BookImages.Add(bookImage);
        _unitOfWork.Save();

        TempData["Success"] = "Image added successfully";

        return RedirectToAction(nameof(Upsert), new { id = bookId });
    }

    public IActionResult DeleteImage(int imageId)
    {
        try
        {
            BookImage image = _unitOfWork.BookImages.GetById(imageId);
            _fileHelper.DeleteFile(image.Url);

            _unitOfWork.BookImages.Remove(image);
            _unitOfWork.Save();

            TempData["Success"] = "Image deleted successfully";

            return RedirectToAction(nameof(Upsert), new { id = image.BookId });
        }
        catch (ModelNotFoundException<BookImage>)
        {
            TempData["Error"] = "Image was not found";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }

    #region API calls

    [HttpGet]
    public IActionResult GetAll()
    {
        var books = _unitOfWork.Books.GetAll();
        var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);

        return Json(new { data = bookDtos });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
            return ErrorDeleteResult();

        Book book = _unitOfWork.Books.GetById((int)id);

        if (book is null)
            return ErrorDeleteResult();

        if (book.Images is not null)
        {
            foreach (BookImage image in book.Images)
            {
                _fileHelper.DeleteFile(image.Url);
                _unitOfWork.BookImages.Remove(image);
            }
        }

        _unitOfWork.Books.Remove(book);
        _unitOfWork.Save();

        return SuccessDeleteResult();
    }

    private JsonResult ErrorDeleteResult()
        => Json(new { success = false, message = "Error while deleting" });

    private JsonResult SuccessDeleteResult()
        => Json(new { success = true, message = "Deleted Successfully" });

    #endregion

    private List<SelectListItem> GetExistingGenresAsSelectListItems()
    {
        return _unitOfWork.Genres.GetAll().Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        }).ToList();
    }

    private ViewResult RedirectToSamePage(BookVM bookVM)
    {
        bookVM.Genres = GetExistingGenresAsSelectListItems();

        return View(bookVM);
    }
}
