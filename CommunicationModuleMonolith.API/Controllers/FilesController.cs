using CommunicationModuleMonolith.API.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace CommunicationModuleMonolith.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<FilesController> _logger;

    public FilesController(IFileStorageService fileStorageService, ILogger<FilesController> logger)
    {
        _fileStorageService = fileStorageService;
        _logger = logger;
    }

    [HttpPut("{fileId}/content")]
    public async Task<IActionResult> UploadContent(string fileId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(fileId, out _))
        {
            _logger.LogWarning("Upload rejected. Invalid file id: {FileId}", fileId);
            return BadRequest("Invalid file id.");
        }

        var correlationId = GetCorrelationId();

        try
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Uploading content for file id: {FileId}", fileId);
            var result = await _fileStorageService.SaveAsync(fileId, Request.Body, cancellationToken);

            stopwatch.Stop();

            _logger.LogInformation(
                "Upload completed for file {FileId} with {BytesWritten} bytes in {ElapsedMilliseconds} ms and correlation id {CorrelationId}",
                fileId,
                result.BytesWritten,
                stopwatch.ElapsedMilliseconds,
                correlationId);

            return Ok(new
            {
                result.FileId,
                result.BytesWritten,
                CorrelationId = correlationId
            });
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning(
                "Upload was cancelled for file {FileId} with correlation id {CorrelationId}",
                fileId,
                correlationId);

            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Upload was cancelled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Upload failed for file {FileId} with correlation id {CorrelationId}",
                fileId,
                correlationId);

            return StatusCode(500, "An error occurred while uploading the file.");
        }
    }

    [HttpGet("{fileId}/content")]
    public async Task<IActionResult> DownloadContent(string fileId, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(fileId, out _))
        {
            _logger.LogWarning("Download rejected. Invalid file id: {FileId}", fileId);
            return BadRequest("Invalid file id.");
        }

        var correlationId = GetCorrelationId();

        try
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Download request for file id: {FileId} with correlation id {CorrelationId}",
                fileId, 
                correlationId);

            var result = await _fileStorageService.OpenReadAsync(fileId, cancellationToken);

            if (result is null)
            {
                stopwatch.Stop();
                _logger.LogWarning(
                    "Download failed because file {FileId} was not found after {ElapsedMilliseconds} ms with correlation id {CorrelationId}",
                    fileId,
                    stopwatch.ElapsedMilliseconds,
                    correlationId);
                return NotFound();
            }
            stopwatch.Stop();

            _logger.LogInformation(
                "Download started for file {FileId} with {ContentLength} bytes after {ElapsedMilliseconds} ms and correlation id {CorrelationId}",
                fileId,
                result.ContentLength,
                stopwatch.ElapsedMilliseconds,
                correlationId);

            return File(result.Content, result.ContentType, enableRangeProcessing: true);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning(
                "Upload was cancelled for file {FileId} with correlation id {CorrelationId}",
                fileId,
                correlationId);

            return StatusCode(StatusCodes.Status499ClientClosedRequest, "Download was cancelled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Download failed for file {FileId} with correlation id {CorrelationId}",
                fileId,
                correlationId);

            return StatusCode(500, "An error occurred while downloading the file.");
        }
    }

    private string GetCorrelationId()
    {
        if (Request.Headers.TryGetValue("X-Correlation-ID", out var correlationId)
            && !string.IsNullOrWhiteSpace(correlationId))
        {
            return correlationId.ToString();
        }
        return Guid.NewGuid().ToString();
    }
}
