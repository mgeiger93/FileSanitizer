using FileSanitizer.BL;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FileSanitizer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileSanitizer : ControllerBase
    {
        private readonly ILogger<FileSanitizer> _logger;
        private readonly ISanitizedFileContentProvider _sanitizedContentProvider;

        public FileSanitizer(ILogger<FileSanitizer> logger,
                             ISanitizedFileContentProvider sanitizedContentProvider)
        {
            _logger = logger;
            _sanitizedContentProvider = sanitizedContentProvider;
        }

        [HttpPost]
        public FileResult Sanitize(IFormFile file)
        {
            string sanitizedFile = _sanitizedContentProvider.GetSanitizedFileContent(file.OpenReadStream(),
                Path.GetExtension(file.FileName));

            return File(Encoding.UTF8.GetBytes(sanitizedFile),
                        file.ContentType);
        }
    }
}
