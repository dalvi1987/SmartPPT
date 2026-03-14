using SmartPPT.Template.Contracts.Templates;
using temp = SmartPPT.Template.Domain.Templates;

namespace SmartPPT.Template.Application.Mappers;

public static class TemplateMapper
{
    public static TemplateDto ToDto(temp.Template template)
    {
        ArgumentNullException.ThrowIfNull(template);

        return new TemplateDto
        {
            Id = template.Id,
            Name = template.Name
        };
    }
}
