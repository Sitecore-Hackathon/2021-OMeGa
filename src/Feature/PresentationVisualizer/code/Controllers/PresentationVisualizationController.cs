using System.Web.Http;
using System.Web.Http.Results;
using Sitecore.Annotations;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Feature.PresentationVisualizer.Data;
using Feature.PresentationVisualizer.Services;

namespace Feature.PresentationVisualizer.Controllers
{
    [RoutePrefix("sitecore/api/presentationvisualization/{itemId}")]
    public class PresentationVisualizationController : ApiController
    {
        private static readonly ID DeviceId = new ID("{FE5D7FDF-89C0-4D99-9AA3-B5FBD009C9F3}");

        [NotNull]
        private readonly IItemPresentationHierarchyBuilder _itemPresentationHierarchyBuilder;

        public PresentationVisualizationController()
            : this(new ItemPresentationHierarchyBuilder())
        {
        }

        public PresentationVisualizationController([NotNull] IItemPresentationHierarchyBuilder itemPresentationHierarchyBuilder)
        {
            Assert.ArgumentNotNull(itemPresentationHierarchyBuilder, nameof(itemPresentationHierarchyBuilder));

            _itemPresentationHierarchyBuilder = itemPresentationHierarchyBuilder;
        }

        [Route]
        [HttpGet]
        public JsonResult<PresentationElement> GetItemPresentation([NotNull] string itemId, [NotNull] string database = "master")
        {
            Assert.ArgumentNotNullOrEmpty(itemId, nameof(itemId));

            Database contextDatabase = Factory.GetDatabase(database);
            Item item = contextDatabase.GetItem(ID.Parse(itemId));
            Assert.IsNotNull(item, nameof(item));

            PresentationElement itemLevel = _itemPresentationHierarchyBuilder.Build(item, GetDeviceItem(contextDatabase));

            return Json(itemLevel);
        }

        private static DeviceItem GetDeviceItem([NotNull] Database database)
        {
            Item innerItem = database.GetItem(DeviceId);
            Assert.IsNotNull(innerItem, nameof(innerItem));

            return new DeviceItem(innerItem);
        }
    }
}