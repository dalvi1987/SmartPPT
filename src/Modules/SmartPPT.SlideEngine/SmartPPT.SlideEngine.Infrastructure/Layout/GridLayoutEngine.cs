using SmartPPT.Template.Contracts.Layouts;

namespace SmartPPT.SlideEngine.Infrastructure.Layout;

public sealed class GridLayoutEngine
{
    public (double X, double Y, double Width, double Height) CalculateGridCell(
        LayoutGridDto gridConfiguration,
        int columnStart,
        int columnSpan,
        int rowStart,
        int rowSpan)
    {
        if (gridConfiguration.Columns <= 0 || gridConfiguration.Rows <= 0)
        {
            return (0d, 0d, 0d, 0d);
        }

        var normalizedColumnSpan = Math.Max(1, columnSpan);
        var normalizedRowSpan = Math.Max(1, rowSpan);
        var safeColumnStart = Math.Max(0, columnStart);
        var safeRowStart = Math.Max(0, rowStart);

        var cellWidth = (100d - (2 * gridConfiguration.Margin) - ((gridConfiguration.Columns - 1) * gridConfiguration.Gutter))
            / gridConfiguration.Columns;
        var cellHeight = (100d - (2 * gridConfiguration.Margin) - ((gridConfiguration.Rows - 1) * gridConfiguration.Gutter))
            / gridConfiguration.Rows;

        var x = gridConfiguration.Margin + (safeColumnStart * (cellWidth + gridConfiguration.Gutter));
        var y = gridConfiguration.Margin + (safeRowStart * (cellHeight + gridConfiguration.Gutter));
        var width = (cellWidth * normalizedColumnSpan) + (gridConfiguration.Gutter * (normalizedColumnSpan - 1));
        var height = (cellHeight * normalizedRowSpan) + (gridConfiguration.Gutter * (normalizedRowSpan - 1));

        return (x, y, width, height);
    }
}
