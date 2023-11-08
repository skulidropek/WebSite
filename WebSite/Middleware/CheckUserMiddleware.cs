using Microsoft.AspNetCore.Identity;

namespace WebSite.Middleware
{
    public class CheckUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<IdentityUser>>();

                    var user = await userManager.GetUserAsync(context.User);
                    if (user == null)
                    {
                        await signInManager.SignOutAsync();
                        foreach (var cookie in context.Request.Cookies.Keys)
                        {
                            context.Response.Cookies.Delete(cookie);
                        }
                    }
                }
            }

            await _next(context);
        }
    }

}
