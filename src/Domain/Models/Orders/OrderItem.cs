using Domain.Models.Books;
using Domain.Models.Carts;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Orders;

#pragma warning disable CS8618 
public class OrderItem : IBookCart
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    [ValidateNever]
    public virtual Book Book { get; set; }

    [Required]
    [Range(1, 1000)]
    public int Count { get; set; }

    [Required]
    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    [ValidateNever]
    public virtual Order Order { get; set; }

    [Required]
    [Range(1, 1000)]
    public double Price { get; set; }

    public double GetTotalPrice() => Count * Price;
}
