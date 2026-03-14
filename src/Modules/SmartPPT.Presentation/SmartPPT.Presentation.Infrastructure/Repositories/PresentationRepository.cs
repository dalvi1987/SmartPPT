using LiteDB;
using SmartPPT.Presentation.Application.Repositories;
using SmartPPT.Presentation.Domain.Presentations;
using SmartPPT.Presentation.Infrastructure.Persistence;

namespace SmartPPT.Presentation.Infrastructure.Repositories;

public sealed class PresentationRepository : IPresentationRepository
{
    private readonly LiteDbContext _context;

    public PresentationRepository(LiteDbContext context)
    {
        _context = context;
    }

    public Presentation? GetPresentation(Guid presentationId)
    {
        return _context.Presentations.FindById(new BsonValue(presentationId));
    }

    public IReadOnlyCollection<Presentation> ListPresentations()
    {
        return _context.Presentations.FindAll().ToList();
    }

    public void SavePresentation(Presentation presentation)
    {
        ArgumentNullException.ThrowIfNull(presentation);

        _context.Presentations.Upsert(presentation);
    }
}
