using Domain.Models.Books;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Areas.Admin.ViewModels;

#pragma warning disable CS8618
public class BookVM
{
    public Book Book { get; set; }

    [ValidateNever]
    public IList<SelectListItem> Genres { get; set; }
}