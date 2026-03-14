using SmartPPT.Template.Domain.Enums;

namespace SmartPPT.Template.Domain.Patterns;

public sealed class PatternElement
{
    public PatternElement(ElementType elementType, string slotName, int maxItems)
    {
        ElementType = elementType;
        SlotName = slotName;
        MaxItems = maxItems;
    }

    public ElementType ElementType { get; private set; }

    public string SlotName { get; private set; }

    public int MaxItems { get; private set; }
}
