using Foundation;
using System.ComponentModel;
using TwitterSafari;
using TwitterSafari.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(iOSEntryRenderer))]
namespace TwitterSafari.iOS
{
    public class iOSEntryRenderer : EntryRenderer 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
        }

    }
}