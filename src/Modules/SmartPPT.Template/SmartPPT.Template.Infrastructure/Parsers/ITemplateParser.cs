using SmartPPT.Template.Domain.Templates;

namespace SmartPPT.Template.Infrastructure.Parsers;

public interface ITemplateParser
{
    Template ParseTemplate(string filePath);
}
