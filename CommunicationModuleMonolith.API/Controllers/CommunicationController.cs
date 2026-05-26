using CommunicationModuleMonolith.API.BLL.Interfaces;
using CommunicationModuleMonolith.API.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommunicationModuleMonolith.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommunicationController : ControllerBase
{
    private readonly ICommunicationService _communicationService;

    public CommunicationController(ICommunicationService communicationService)
    {
        _communicationService = communicationService;
    }

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        var result = _communicationService.GetStatus();

        return Ok(result);
    }

    [HttpPost("send")]
    public IActionResult Send([FromBody] CommunicationRequest request)
    {
        var response = _communicationService.ProcessRequest(request);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
