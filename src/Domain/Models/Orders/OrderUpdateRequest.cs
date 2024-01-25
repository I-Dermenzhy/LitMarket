using Domain.Models.Users;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Orders;

#pragma warning disable CS8618 
public class OrderUpdateRequest : IModel
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    [ValidateNever]
    public Order Order { get; set; }

    public string? CustomerNameUpdate { get; set; }
    public FullAddress? ShippingAddressUpdate { get; set; }

    public string? Reason { get; set; }

    public UpdateRequestStatus Status { get; set; }

    public static OrderUpdateRequest Create(Order current, Order updateRequest,
        string? reason, UpdateRequestStatus status = UpdateRequestStatus.Pending)
    {
        FullAddress currentAddress = current.Shipping.ShippingAddress;
        FullAddress requestedAddress = updateRequest.Shipping.ShippingAddress;

        FullAddress? shippingAddressUpdate = currentAddress.Equals(requestedAddress)
            ? null : requestedAddress;

        string? customerNameUpdate = current.Customer.Name.Equals(updateRequest.Customer.Name)
            ? null : updateRequest.Customer.Name;

        return new OrderUpdateRequest()
        {
            OrderId = current.Id,
            Order = current,
            CustomerNameUpdate = customerNameUpdate,
            ShippingAddressUpdate = shippingAddressUpdate,
            Reason = reason,
            Status = status
        };
    }
}

public enum UpdateRequestStatus
{
    Pending,
    Approved,
    Rejected
}