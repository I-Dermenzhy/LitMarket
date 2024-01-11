namespace Domain.Dto.Books;

#pragma warning disable CS8618 
public class BookDto
{
    public int Id { get; set; }

    public string Author { get; set; }

    public int CategoryId { get; set; }

    public BookCategoryDto Category { get; set; }

    public string? Description { get; set; }

    public IEnumerable<BookImageDto> Images { get; set; }

    public string ISBN { get; set; }

    public int PriceListId { get; set; }

    public PriceListDto PriceList { get; set; }

    public string Title { get; set; }
}
