using SmartPPT.Template.Application.Interfaces;
using SmartPPT.Template.Domain.Templates;
using SmartPPT.Template.Infrastructure.Persistence;

namespace SmartPPT.Template.Infrastructure.Repositories;

public sealed class TemplateRepository : ITemplateRepository
{
    private readonly LiteDbContext _context;

    public TemplateRepository(LiteDbContext context)
    {
        _context = context;
    }

    public Template? GetTemplateById(Guid templateId)
    {
        return _context.Templates.FindById(templateId);
    }

    public IReadOnlyCollection<Template> GetAllTemplates()
    {
        return _context.Templates.FindAll().ToArray();
    }
}
