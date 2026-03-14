using LiteDB;
using SmartPPT.Presentation.Application.Repositories;
using prsnt = SmartPPT.Presentation.Domain.Presentations;
using SmartPPT.Presentation.Infrastructure.Persistence;

namespace SmartPPT.Presentation.Infrastructure.Repositories;

public sealed class PresentationRepository : IPresentationRepository
{
    private readonly LiteDbContext _context;

    public PresentationRepository(LiteDbContext context)
    {
        _context = context;
    }

    public prsnt.Presentation? GetPresentation(Guid presentationId)
    {
        return _context.Presentations.FindById(new BsonValue(presentationId));
    }

    public IReadOnlyCollection<prsnt.Presentation> ListPresentations()
    {
        return _context.Presentations.FindAll().ToList();
    }

    public void SavePresentation(prsnt.Presentation presentation)
    {
        ArgumentNullException.ThrowIfNull(presentation);

        _context.Presentations.Upsert(presentation);
    }
}
