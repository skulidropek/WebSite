using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebSite.Middleware
{
    public class WebSiteSettingMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSiteSettingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string roleName = "root";

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = roleName,
                    });
                }

                var usersInRole = await userManager.GetUsersInRoleAsync(roleName);

                if (usersInRole == null || usersInRole.Count == 0)
                {
                    if (!context.Request.Path.StartsWithSegments("/websitesetting") && !context.Request.Path.StartsWithSegments("/_blazor"))
                    {
                        context.Response.Redirect("/websitesetting/adminusercreate");
                        return;
                    }
                }
                else if (context.Request.Path.StartsWithSegments("/websitesetting"))
                {
                    context.Response.Redirect("/");
                    return;
                }

                await _next(context);
            }
        }
    }

}
