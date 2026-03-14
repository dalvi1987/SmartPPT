namespace SmartPPT.Template.Contracts.Layouts;

public sealed class LayoutGridDto
{
    public int Columns { get; init; }

    public int Rows { get; init; }

    public double Gutter { get; init; }

    public double Margin { get; init; }
}
