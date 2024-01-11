using Domain.Models.Products;

namespace Domain.Dto.Books;

#pragma warning disable CS8618 
public class BookImageDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public BookImageType ImageType { get; set; }
    public string Url { get; set; }
}