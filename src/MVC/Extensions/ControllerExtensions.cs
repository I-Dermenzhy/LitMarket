using Domain.Models.Users;

using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace MVC.Extensions;

public static class ControllerExtensions
{
    public static string GetUserId(this Controller controller)
    {
        if (!controller.User.Identity!.IsAuthenticated)
            return string.Empty;

        return GetUserClaim(controller)!.Value;
    }

    public static Claim? GetUserClaim(this Controller controller)
    {
        var claimsIdentity = (ClaimsIdentity)controller.User.Identity!;

        return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
    }

    public static bool IsCompany(this Controller controller)
    {
        if (!controller.User.Identity!.IsAuthenticated)
            return false;

        return controller.User.IsInRole(Roles.Company);
    }
}
