using SmartPPT.Template.Contracts.Layouts;
using SmartPPT.Template.Contracts.Patterns;

namespace SmartPPT.SlideEngine.Application.Interfaces;

public interface ILayoutCalculator
{
    (double X, double Y, double Width, double Height) CalculatePosition(
        LayoutGridDto gridConfiguration,
        PatternElementDto patternSlot,
        int elementIndex);
}
