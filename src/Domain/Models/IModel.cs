using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public interface IModel<T>
{
    [Key]
    public T Id { get; set; }
}

public interface IModel : IModel<int>
{
}