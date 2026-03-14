using SmartPPT.SlideEngine.Contracts.Layout;
using SmartPPT.SlideEngine.Contracts.Slides;

namespace SmartPPT.SlideEngine.Contracts.Services;

public interface ISlideLayoutEngine
{
    RenderableSlideDto GenerateLayout(SlideDto slide);
}
