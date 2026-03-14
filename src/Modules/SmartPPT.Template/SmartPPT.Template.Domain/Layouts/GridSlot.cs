namespace SmartPPT.Template.Domain.Layouts;

public sealed class GridSlot
{
    public GridSlot(int columnStart, int columnSpan, int rowStart, int rowSpan)
    {
        ColumnStart = columnStart;
        ColumnSpan = columnSpan;
        RowStart = rowStart;
        RowSpan = rowSpan;
    }

    public int ColumnStart { get; private set; }

    public int ColumnSpan { get; private set; }

    public int RowStart { get; private set; }

    public int RowSpan { get; private set; }
}
