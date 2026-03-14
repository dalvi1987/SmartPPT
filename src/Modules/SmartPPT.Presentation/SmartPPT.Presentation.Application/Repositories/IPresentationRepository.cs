using prsnt = SmartPPT.Presentation.Domain.Presentations;

namespace SmartPPT.Presentation.Application.Repositories;

public interface IPresentationRepository
{
    prsnt.Presentation? GetPresentation(Guid presentationId);

    IReadOnlyCollection<prsnt.Presentation> ListPresentations();

    void SavePresentation(prsnt.Presentation presentation);
}
