using Microsoft.AspNetCore.Identity;
using MyMedia.Data.Models;

namespace MyMedia.Gestao.Components.Account;

internal sealed class ApplicationUserAccessor(UserManager<ApplicationUser> userManager, IdentityRedirectManager redirectManager) {
    public async Task<ApplicationUser> GetRequiredUserAsync(HttpContext context) {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null) {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
        }

        return user;
    }
}