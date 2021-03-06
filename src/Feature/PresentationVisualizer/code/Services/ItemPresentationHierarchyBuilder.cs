using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Annotations;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Layouts;
using Feature.PresentationVisualizer.Data;
using DataSource = Feature.PresentationVisualizer.Data.DataSource;

namespace Feature.PresentationVisualizer.Services
{
    public class ItemPresentationHierarchyBuilder : IItemPresentationHierarchyBuilder
    {
        private const char PlaceholderSeparator = '/';

        private const string PlaceholderType = "placeholder";

        private const string RenderingType = "rendering";

        public PresentationElement Build(Item item, DeviceItem device)
        {
            Assert.ArgumentNotNull(item, nameof(item));
            Assert.ArgumentNotNull(device, nameof(device));

            PresentationElement itemLevel = CreateRoot(item);

            PresentationElement deviceLevel = CreateDeviceRoot(device);
            itemLevel.Children.Add(deviceLevel);

            RenderingReference[] renderingReferences = GetItemRenderings(item, device);
            ProcessRenderingReferences(renderingReferences, deviceLevel, item.Database);

            return itemLevel;
        }
        
        [NotNull]
        private static PresentationElement CreateRoot([NotNull] Item item)
        {
            return new PresentationElement(item.DisplayName, "item", null, Enumerable.Empty<PresentationElement>().ToList());
        }

        [NotNull]
        private static PresentationElement CreateDeviceRoot([NotNull] DeviceItem device)
        {
            return new PresentationElement(device.DisplayName, "device", null, Enumerable.Empty<PresentationElement>().ToList());
        }

        [NotNull]
        private static PresentationElement GetOrCreatePlaceholder([NotNull] string placeholderKey, [NotNull] PresentationElement parent)
        {
            PresentationElement placeholder = parent.Children.FirstOrDefault(x => x.Type == PlaceholderType && string.Equals(x.Name, placeholderKey, StringComparison.OrdinalIgnoreCase));
            if(placeholder != null)
            {
                return placeholder;
            }

            placeholder = new PresentationElement(placeholderKey, PlaceholderType, null, Enumerable.Empty<PresentationElement>().ToList());
            parent.Children.Add(placeholder);

            return placeholder;
        }

        private static void ProcessRenderingReferences([NotNull] RenderingReference[] renderingReferences, [NotNull] PresentationElement deviceLevel, [NotNull] Database database)
        {
            foreach(RenderingReference renderingReference in renderingReferences)
            {
                ProcessRenderingReference(renderingReference, deviceLevel, database);
            }
        }

        private static void ProcessRenderingReference([NotNull] RenderingReference renderingReference, [NotNull] PresentationElement deviceLevel, [NotNull] Database database)
        {
            string placeholderKey = NormalizePlaceholderKey(renderingReference.Placeholder);
            PresentationElement placeholder = GetOrCreatePlaceholder(placeholderKey, deviceLevel);
            PresentationElement rendering = BuildRendering(renderingReference, database);

            placeholder.Children.Add(rendering);
        }

        [NotNull]
        private static PresentationElement BuildRendering([NotNull] RenderingReference renderingReference, [NotNull] Database database)
        {
            DataSource dataSource = BuildDataSource(renderingReference.Settings.DataSource, database);

            PresentationElement rendering = new PresentationElement(renderingReference.RenderingItem.DisplayName, RenderingType, dataSource, Enumerable.Empty<PresentationElement>().ToList())
            {
                Parameters = BuildRenderingParameters(renderingReference)
            };

            return rendering;
        }

        [CanBeNull]
        private static DataSource BuildDataSource([CanBeNull] string dataSource, [NotNull] Database database)
        {
            if(string.IsNullOrEmpty(dataSource))
            {
                return null;
            }

            Item dataSourceItem = null;
            if(ID.TryParse(dataSource, out ID id))
            {
                dataSourceItem = database.GetItem(id);
            }
            else
            {
                dataSourceItem = database.GetItem(dataSource);
            }

            if(dataSourceItem == null)
            {
                return null;
            }

            return new DataSource(dataSourceItem.ID, dataSourceItem.DisplayName, dataSourceItem.Paths.FullPath);
        }

        [NotNull]
        private static IReadOnlyCollection<RenderingParameter> BuildRenderingParameters([NotNull] RenderingReference renderingReference)
        {
            if(string.IsNullOrEmpty(renderingReference.Settings.Parameters))
            {
                return null;
            }

            var parameters = HttpUtility.ParseQueryString(renderingReference.Settings.Parameters);

            return parameters.AllKeys.Select(x => new RenderingParameter(x, parameters[x])).ToList();
        }

        [NotNull]
        private static RenderingReference[] GetItemRenderings([NotNull] Item item, [NotNull] DeviceItem device)
        {
            return item.Visualization.GetRenderings(device, false);
        }

        private static string NormalizePlaceholderKey([NotNull] string placeholderKey)
        {
            return placeholderKey.ToLowerInvariant().TrimStart(PlaceholderSeparator);
        }
    }
}