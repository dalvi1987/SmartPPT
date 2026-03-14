using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.Storage.Domain.Files;

namespace SmartPPT.Storage.Application.Builders;

public interface IDocumentBuilder
{
    StoredFile BuildDocument(RenderableSlideDto slide);
}
