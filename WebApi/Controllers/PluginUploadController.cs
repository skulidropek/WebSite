using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PluginUploadController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, string user)
        {
            using (var stream = file.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                string data = await reader.ReadToEndAsync();

                System.IO.File.WriteAllText(Path.Combine(GetBase64String(user), file.Name), data);
            }

            return Ok();
        }
        
        [HttpGet]
        [Route("GetErrors")]
        public async Task<IActionResult> GetErrors(string user)
        {
            var folder = GetBase64String(user);

            return Ok();
        }

        private string GetBase64String(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
