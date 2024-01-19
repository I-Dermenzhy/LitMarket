using Domain.Models.Books;
using Domain.Models.Users;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Carts;

#pragma warning disable CS8618
public class BookCart : IBookCart
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    [ValidateNever]
    public virtual Book Book { get; set; }

    [Required]
    [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
    public int Count { get; set; }

    [Required]
    public string CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    [ValidateNever]
    public virtual Customer Customer { get; set; }

    public double GetPricePerBook() => Count switch
    {
        < 50 => Book.PriceList.Price,
        < 100 => Book.PriceList.Price50,
        _ => Book.PriceList.Price100
    };

    public double GetTotalPrice() => GetPricePerBook() * Count;
}