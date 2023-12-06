using LitMarket.Domain.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
public class Category : IModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Range(1, 100)]
    [DisplayName("Display Order")]
    public int DisplayOrder { get; set; }
}
