using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoslynLibrary.Services;
using System.Text;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PluginController : ControllerBase
    {
        private readonly PluginDiagnosticsAnalyzerService _pluginDiagnosticsAnalyzerService;

        public PluginController(PluginDiagnosticsAnalyzerService pluginDiagnosticsAnalyzerService)
        {
            _pluginDiagnosticsAnalyzerService = pluginDiagnosticsAnalyzerService;
        }

        [HttpGet]
        [Route("Compile")]
        public async Task<IActionResult> Compile(string plugin)
        {
            return Ok(await _pluginDiagnosticsAnalyzerService.AnalyzeCompilationAsync(plugin));
        }
    }
}
