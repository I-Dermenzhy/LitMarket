using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Orders;

#pragma warning disable CS8618 
public class OrderUpdateRequest : Order
{
    public int CurrentOrderId { get; set; }

    [ForeignKey(nameof(CurrentOrderId))]
    [ValidateNever]
    public Order CurrentOrder { get; set; }
}
