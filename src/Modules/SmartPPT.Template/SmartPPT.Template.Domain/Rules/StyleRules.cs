namespace SmartPPT.Template.Domain.Rules;

public sealed class StyleRules
{
    public StyleRules(
        double marginTop,
        double marginBottom,
        double marginLeft,
        double marginRight,
        double defaultFontSize)
    {
        MarginTop = marginTop;
        MarginBottom = marginBottom;
        MarginLeft = marginLeft;
        MarginRight = marginRight;
        DefaultFontSize = defaultFontSize;
    }

    public double MarginTop { get; private set; }

    public double MarginBottom { get; private set; }

    public double MarginLeft { get; private set; }

    public double MarginRight { get; private set; }

    public double DefaultFontSize { get; private set; }
}
