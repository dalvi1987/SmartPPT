using SmartPPT.Presentation.Domain.Presentations;

namespace SmartPPT.Presentation.Application.Repositories;

public interface IPresentationRepository
{
    Presentation? GetPresentation(Guid presentationId);

    IReadOnlyCollection<Presentation> ListPresentations();

    void SavePresentation(Presentation presentation);
}
