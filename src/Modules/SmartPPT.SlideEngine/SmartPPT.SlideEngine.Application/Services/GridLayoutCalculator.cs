using SmartPPT.SlideEngine.Application.Interfaces;
using SmartPPT.Template.Contracts.Layouts;
using SmartPPT.Template.Contracts.Patterns;

namespace SmartPPT.SlideEngine.Application.Services;

public sealed class GridLayoutCalculator : ILayoutCalculator
{
    public (double X, double Y, double Width, double Height) CalculatePosition(
        LayoutGridDto gridConfiguration,
        PatternElementDto patternSlot,
        int elementIndex)
    {
        if (gridConfiguration.Columns <= 0 || gridConfiguration.Rows <= 0)
        {
            return (0d, 0d, 0d, 0d);
        }

        // Placeholder grid-based calculation until slot-specific rules are implemented.
        var cellWidth = (100d - (2 * gridConfiguration.Margin) - ((gridConfiguration.Columns - 1) * gridConfiguration.Gutter))
            / gridConfiguration.Columns;
        var cellHeight = (100d - (2 * gridConfiguration.Margin) - ((gridConfiguration.Rows - 1) * gridConfiguration.Gutter))
            / gridConfiguration.Rows;

        var column = elementIndex % gridConfiguration.Columns;
        var row = elementIndex / gridConfiguration.Columns;

        var x = gridConfiguration.Margin + (column * (cellWidth + gridConfiguration.Gutter));
        var y = gridConfiguration.Margin + (row * (cellHeight + gridConfiguration.Gutter));

        return (x, y, cellWidth, cellHeight);
    }
}
