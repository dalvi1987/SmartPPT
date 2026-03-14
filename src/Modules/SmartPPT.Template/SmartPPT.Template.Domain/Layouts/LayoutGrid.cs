namespace SmartPPT.Template.Domain.Layouts;

public sealed class LayoutGrid
{
    public LayoutGrid(int columns, int rows)
    {
        Columns = columns;
        Rows = rows;
    }

    public int Columns { get; private set; }

    public int Rows { get; private set; }
}
