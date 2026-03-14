using temp = SmartPPT.Template.Domain.Templates;

namespace SmartPPT.Template.Infrastructure.Parsers;

public interface ITemplateParser
{
    temp.Template ParseTemplate(string filePath);
}
