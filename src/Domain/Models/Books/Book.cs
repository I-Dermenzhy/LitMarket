using Domain.Models.Products;

using LitMarket.Domain.Models;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Books;

#pragma warning disable CS8618 
public class Book : IModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Author { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(CategoryId))]
    public BookCategory Category { get; set; }

    [MaxLength(10000)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(20)]
    public string ISBN { get; set; }

    [Required]
    public int PriceListId { get; set; }

    [ValidateNever]
    [ForeignKey(nameof(PriceListId))]
    public PriceList PriceList { get; set; }

    [Required]
    [MaxLength(80)]
    public string Title { get; set; }
}