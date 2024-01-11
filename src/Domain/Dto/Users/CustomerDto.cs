using Domain.Models.Users;

namespace Domain.Dto.Users;

#pragma warning disable CS8618 
public class CustomerDto
{
    public string Id { get; set; }
    public CustomerType CustomerType { get; set; }
    public string Email { get; set; }
    public FullAddress FullAddress { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
}