using Abstractions.Repositories;

using AutoMapper;

using DataAccess.Exceptions;

using Domain.Dto.Books;
using Domain.Models.Books;
using Domain.Models.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public sealed class GenreController(IUnitOfWork unitOfWork, IMapper mapper) : Controller
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public IActionResult Index()
    {
        var genres = _unitOfWork.Genres.GetAll();

        return View(genres);
    }

    public IActionResult Upsert(int? id)
    {
        Genre genre;

        if (id is not null and not 0)
            //update
            genre = _unitOfWork.Genres.GetById((int)id);
        else
            //create
            genre = new();

        return View(genre);
    }

    [HttpPost]
    public IActionResult Upsert(Genre genre)
    {
        if (!ModelState.IsValid)
            return View(genre);

        if (genre.Id == 0)
            _unitOfWork.Genres.Add(genre);
        else
            _unitOfWork.Genres.Update(genre);

        _unitOfWork.Save();

        TempData["Success"] = "BookCategory created successfully";

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        if (id == 0)
            return NotFound();

        try
        {
            Genre genre = _unitOfWork.Genres.GetById(id);

            return View(genre);
        }
        catch (NotFoundException<Genre>)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult Delete(Genre genre)
    {
        try
        {
            _unitOfWork.Genres.Remove(genre);
            _unitOfWork.Save();

            TempData["Success"] = "BookCategory deleted successfully";

            return RedirectToAction(nameof(Index));

        }
        catch (NotFoundException<Genre>)
        {
            return NotFound();
        }
    }

    #region API calls

    [HttpGet]
    public IActionResult GetAll()
    {
        var genres = _unitOfWork.Genres.GetAll();
        var genreDtos = _mapper.Map<IEnumerable<GenreDto>>(genres);

        return Json(new { data = genreDtos });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
            return ErrorDeleteResult();

        Genre genre = _unitOfWork.Genres.GetById((int)id);

        if (genre is null)
            return ErrorDeleteResult();

        _unitOfWork.Genres.Remove(genre);
        _unitOfWork.Save();

        return SuccessDeleteResult();
    }

    private JsonResult ErrorDeleteResult()
        => Json(new { success = false, message = "Error while deleting" });

    private JsonResult SuccessDeleteResult()
        => Json(new { success = true, message = "Deleted Successfully" });

    #endregion
}
