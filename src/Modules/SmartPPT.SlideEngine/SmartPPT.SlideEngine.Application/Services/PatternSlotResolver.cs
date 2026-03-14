using SmartPPT.SlideEngine.Contracts.Elements;
using SmartPPT.Template.Contracts.Patterns;

namespace SmartPPT.SlideEngine.Application.Services;

public sealed class PatternSlotResolver
{
    public IReadOnlyList<(SlideElementDto Element, PatternElementDto Slot)> ResolveSlots(
        IReadOnlyCollection<SlideElementDto> elements,
        IReadOnlyCollection<PatternElementDto> patternSlots)
    {
        var remainingSlots = patternSlots.ToList();
        var resolved = new List<(SlideElementDto Element, PatternElementDto Slot)>();

        foreach (var element in elements)
        {
            var matchedSlot = remainingSlots.FirstOrDefault(slot =>
                string.Equals(slot.SlotName, element.SlotName, StringComparison.OrdinalIgnoreCase));

            matchedSlot ??= remainingSlots.FirstOrDefault(slot =>
                string.Equals(slot.ElementType.ToString(), element.ElementType.ToString(), StringComparison.OrdinalIgnoreCase));

            if (matchedSlot is null)
            {
                continue;
            }

            resolved.Add((element, matchedSlot));
            remainingSlots.Remove(matchedSlot);
        }

        return resolved;
    }
}
