using Microsoft.AspNetCore.Mvc;
using SmartPPT.Presentation.Contracts.Requests;
using SmartPPT.Presentation.Contracts.Responses;
using SmartPPT.Presentation.Contracts.Services;

namespace SmartPPT.API.Controllers;

[ApiController]
[Route("presentations")]
public sealed class PresentationsController : ControllerBase
{
    private readonly IPresentationService _presentationService;

    public PresentationsController(IPresentationService presentationService)
    {
        _presentationService = presentationService;
    }

    [HttpPost("generate")]
    [ProducesResponseType(typeof(GeneratePresentationResponse), StatusCodes.Status200OK)]
    public ActionResult<GeneratePresentationResponse> GeneratePresentation([FromBody] GeneratePresentationRequest request)
    {
        var response = _presentationService.GeneratePresentation(request);
        return Ok(response);
    }
}
