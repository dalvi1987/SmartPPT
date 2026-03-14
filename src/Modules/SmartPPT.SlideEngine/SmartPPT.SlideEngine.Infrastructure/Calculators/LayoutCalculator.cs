using SmartPPT.SlideEngine.Application.Interfaces;
using SmartPPT.SlideEngine.Infrastructure.Layout;
using SmartPPT.Template.Contracts.Layouts;
using SmartPPT.Template.Contracts.Patterns;

namespace SmartPPT.SlideEngine.Infrastructure.Calculators;

public sealed class LayoutCalculator : ILayoutCalculator
{
    private readonly GridLayoutEngine _gridLayoutEngine;

    public LayoutCalculator(GridLayoutEngine gridLayoutEngine)
    {
        _gridLayoutEngine = gridLayoutEngine;
    }

    public (double X, double Y, double Width, double Height) CalculatePosition(
        LayoutGridDto gridConfiguration,
        PatternElementDto patternSlot,
        int elementIndex)
    {
        var column = gridConfiguration.Columns == 0 ? 0 : elementIndex % gridConfiguration.Columns;
        var row = gridConfiguration.Columns == 0 ? 0 : elementIndex / gridConfiguration.Columns;

        // Minimal placeholder mapping until template patterns expose concrete grid coordinates.
        return _gridLayoutEngine.CalculateGridCell(
            gridConfiguration,
            column,
            1,
            row,
            1);
    }
}
