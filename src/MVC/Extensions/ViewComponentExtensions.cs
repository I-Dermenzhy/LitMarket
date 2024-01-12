using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace MVC.Extensions;

public static class ViewComponentExtensions
{
    public static string GetUserId(this ViewComponent viewComponent)
    {
        if (!viewComponent.User.Identity!.IsAuthenticated)
            return string.Empty;

        return GetUserClaim(viewComponent)!.Value;
    }

    public static Claim? GetUserClaim(this ViewComponent viewComponent)
    {
        var claimsIdentity = (ClaimsIdentity)viewComponent.User.Identity!;

        return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
    }
}
