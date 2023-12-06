using Domain.Models.Books;

using LitMarket.Domain.Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Products;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Product : IModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(BookId))]
    public Book Book { get; set; }

    [Required]
    public int PriceListId { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(PriceListId))]
    public PriceList PriceList { get; set; }

    [MaxLength(100)]
    public string? ImageUrl { get; set; }
}
