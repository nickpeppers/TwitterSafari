using TwitterSafari;
using TwitterSafari.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]
namespace TwitterSafari.Droid
{
    public class HtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            if(Control != null)
            {
                Control.Clickable =
                   Control.LinksClickable = true;
                Control.AutoLinkMask = Android.Text.Util.MatchOptions.All;
            }
        }
    }
}