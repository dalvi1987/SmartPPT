using Microsoft.AspNetCore.Mvc;
using SmartPPT.Presentation.Contracts.Requests;
using SmartPPT.Presentation.Contracts.Responses;
using SmartPPT.Presentation.Contracts.Services;
using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.Storage.Application.Builders;
using SmartPPT.Storage.Domain.Files;

namespace SmartPPT.API.Controllers;

[ApiController]
[Route("presentations")]
public sealed class PresentationsController : ControllerBase
{
    private readonly IPresentationService _presentationService;
    private readonly IDocumentBuilder _slideBuilderService;

    public PresentationsController(IPresentationService presentationService, IDocumentBuilder slideBuilderService)
    {
        _presentationService = presentationService;
        _slideBuilderService = slideBuilderService;
    }

    [HttpPost("generate")]
    [ProducesResponseType(typeof(GeneratePresentationResponse), StatusCodes.Status200OK)]
    public ActionResult<GeneratePresentationResponse> GeneratePresentation([FromBody] GeneratePresentationRequest request)
    {
        var response = _presentationService.GeneratePresentation(request);        
        return Ok(response);
    }

    [HttpPost("test")]
    [ProducesResponseType(typeof(StoredFile), StatusCodes.Status200OK)]
    public ActionResult<StoredFile> GeneratePresentation()
    {
        var lstSlides = new List<RenderableSlideDto>();
        lstSlides.Add(new RenderableSlideDto
        {
            Title = "SmartPPT Demo",
            PositionedElements = new List<PositionedElementDto>
            {
                new PositionedElementDto
                {
                    ElementType = SlideEngine.Contracts.Enums.ElementType.Text,
                    Content = "Welcome to SmartPPT, your AI-powered presentation assistant!",
                    // X = 100, 
                    // Y = 150,
                    // Width = 400, 
                    // Height = 100 
                },
                new PositionedElementDto
                {
                    ElementType = SlideEngine.Contracts.Enums.ElementType.Text,
                    Content = "Welcome to SmartPPT, Shekhar!",
                    // X = 100, 
                    // Y = 150,
                    // Width = 400, 
                    // Height = 100 
                }
            },
        });
        var document = _slideBuilderService.BuildDocument(lstSlides);
        return Ok(document);
    }    


}
