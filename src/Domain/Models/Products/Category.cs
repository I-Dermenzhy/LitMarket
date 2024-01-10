using LitMarket.Domain.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Books;

#pragma warning disable CS8618 
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
