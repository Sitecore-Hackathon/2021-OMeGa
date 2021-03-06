using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Text;
using Sitecore.Web.UI.Sheer;

namespace Feature.PresentationVisualizer.Commands
{
    public class Visualize : Command
    {
        /// <summary>Executes the command in the specified context.</summary>
        /// <param name="context">The context.</param>
        public override void Execute(CommandContext context)
        {
            Assert.ArgumentNotNull((object)context, nameof(context));

            if (context.Items.Length != 1)
                return;

            Item obj = context.Items[0];

            UrlString urlString = new UrlString(UIUtil.GetUri("/sitecore modules/Web/PresentationVisualizer/Visualization.aspx"));
            urlString.Append("id", obj.ID.ToString());
            SheerResponse.ShowModalDialog(urlString.ToString());
        }
    }
}