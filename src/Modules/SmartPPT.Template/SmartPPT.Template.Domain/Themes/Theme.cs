namespace SmartPPT.Template.Domain.Themes;

public sealed class Theme
{
    public Theme(
        string primaryColor,
        string secondaryColor,
        string titleFont,
        string bodyFont)
    {
        PrimaryColor = primaryColor;
        SecondaryColor = secondaryColor;
        TitleFont = titleFont;
        BodyFont = bodyFont;
    }

    public string PrimaryColor { get; private set; }

    public string SecondaryColor { get; private set; }

    public string TitleFont { get; private set; }

    public string BodyFont { get; private set; }
}
