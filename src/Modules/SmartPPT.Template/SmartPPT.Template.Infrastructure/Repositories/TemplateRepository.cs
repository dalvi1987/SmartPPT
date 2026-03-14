using SmartPPT.Template.Application.Interfaces;
using temp = SmartPPT.Template.Domain.Templates;
using SmartPPT.Template.Infrastructure.Persistence;

namespace SmartPPT.Template.Infrastructure.Repositories;

public sealed class TemplateRepository : ITemplateRepository
{
    private readonly LiteDbContext _context;

    public TemplateRepository(LiteDbContext context)
    {
        _context = context;
    }

    public temp.Template? GetTemplateById(Guid templateId)
    {
        return _context.Templates.FindById(templateId);
    }

    public IReadOnlyCollection<temp.Template> GetAllTemplates()
    {
        return _context.Templates.FindAll().ToArray();
    }
}
