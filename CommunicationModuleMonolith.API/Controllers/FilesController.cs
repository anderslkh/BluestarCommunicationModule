using CommunicationModuleMonolith.API.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationModuleMonolith.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;

    public FilesController(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPut("{fileId}/content")]
    public async Task<IActionResult> UploadContent(string fileId, CancellationToken cancellationToken)
    {
        await _fileStorageService.SaveAsync(fileId, Request.Body, cancellationToken);

        return Ok();
    }

    [HttpGet("{fileId}/content")]
    public async Task<IActionResult> DownloadContent(string fileId, CancellationToken cancellationToken)
    {
        var stream = await _fileStorageService.OpenReadAsync(fileId, cancellationToken);

        if (stream is null)
        {
            return NotFound();
        }

        return File(stream, "application/octet-stream", enableRangeProcessing: true);
    }
}
