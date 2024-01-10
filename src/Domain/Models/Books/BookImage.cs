using Domain.Models.Books;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Products;

#pragma warning disable CS8618 
public class BookImage : IModel
{
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; }

    [Required]
    public BookImageType ImageType { get; set; }

    [Required]
    public string Url { get; set; }
}

public enum BookImageType
{
    FrontCover,
    BackCover,
    Content,
}
