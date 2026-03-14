using temp = SmartPPT.Template.Domain.Templates;

namespace SmartPPT.Template.Application.Interfaces;

public interface ITemplateRepository
{
    temp.Template? GetTemplateById(Guid templateId);

    IReadOnlyCollection<temp.Template> GetAllTemplates();
}
