using Sitecore.Data.Items;
using Sitecore.Annotations;
using Feature.PresentationVisualizer.Data;

namespace Feature.PresentationVisualizer.Services
{
    public interface IItemPresentationHierarchyBuilder
    {
        [CanBeNull]
        PresentationElement Build([NotNull] Item item, [NotNull] DeviceItem device);
    }
}
