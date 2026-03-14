using SmartPPT.Template.Contracts.Layouts;
using SmartPPT.Template.Contracts.Rules;
using SmartPPT.Template.Domain.Layouts;
using SmartPPT.Template.Domain.Rules;

namespace SmartPPT.Template.Application.Mappers;

public static class LayoutMapper
{
    public static LayoutGridDto ToDto(LayoutGrid layoutGrid)
    {
        ArgumentNullException.ThrowIfNull(layoutGrid);

        return new LayoutGridDto
        {
            Columns = layoutGrid.Columns,
            Rows = layoutGrid.Rows,
            Gutter = 0,
            Margin = 0
        };
    }

    public static StyleRulesDto ToDto(StyleRules styleRules)
    {
        ArgumentNullException.ThrowIfNull(styleRules);

        return new StyleRulesDto
        {
            MarginTop = styleRules.MarginTop,
            MarginBottom = styleRules.MarginBottom,
            MarginLeft = styleRules.MarginLeft,
            MarginRight = styleRules.MarginRight,
            DefaultFontSize = styleRules.DefaultFontSize
        };
    }
}
