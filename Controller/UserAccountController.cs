using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace TestDevOpsURL.Controller
{
    public class UserAccountController : ControllerBase
    {
        [HttpGet("Login")]
        public async Task<ActionResult> Login([FromQuery] string returnUrl)
        {
            try
            {
                var redirectUri = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;

                if (User.Identity.IsAuthenticated)
                {
                    return LocalRedirect(redirectUri);
                }
                else
                {
                    return Challenge();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // This is the method the Logout button should get to when clicked.
        [HttpGet("Logout")]
        public async Task<ActionResult> Logout([FromQuery] string returnUrl)
        {
            var redirectUri = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;

            if (!User.Identity!.IsAuthenticated)
            {
                return LocalRedirect(redirectUri);
            }

            await HttpContext.SignOutAsync();

            return LocalRedirect(redirectUri);
        }
    }
}