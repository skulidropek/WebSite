using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.RegularExpressions;
using WebSite.Extension;
using WebSite.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WebSite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly DataBaseContextService _dataBaseContext;
        private readonly NavigationManager _navigationManager;

        public ResourceController(DataBaseContextService dataBaseContext, NavigationManager navigationManager)
        {
            _dataBaseContext = dataBaseContext;
            _navigationManager = navigationManager;
        }

        [HttpGet("Download")]
        public async Task<IActionResult> Download(Guid resourceId)
        {
            var resource = await _dataBaseContext.Resources.FirstOrDefaultAsync(s => s.Id == resourceId);

            if(resource == null)
                return NotFound();
            
            var bytes = await Resource.ReadAllBytesAsync(resource.UrlOrPatch);

            // Получить имя файла
            var fileName = Path.GetFileName(resource.UrlOrPatch);

            // Установить заголовок Content-Disposition
            Response.Headers["Content-Disposition"] = $"attachment; filename={fileName}";

            string url = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host.Value;

            var plugin = Regex.Replace(Encoding.UTF8.GetString(bytes), @"(\[Info\(\s*"".*""\s*,\s*"").*(""\s*,\s*"".*""(,"".*"")?\)\])", $"/* Плагин был скачан с сайта: {url} */ $1 {url} $2");

            return File(Encoding.UTF8.GetBytes(plugin), "application/octet-stream");
        }
    }
}
