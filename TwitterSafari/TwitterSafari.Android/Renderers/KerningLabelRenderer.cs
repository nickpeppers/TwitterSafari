using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.OS;
using TwitterSafari;
using TwitterSafari.Droid;

[assembly: ExportRenderer(typeof(KerningLabel), typeof(KerningLabelRenderer))]
namespace TwitterSafari.Droid
{
    public class KerningLabelRenderer : LabelRenderer
    {
        public KerningLabelRenderer()
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null && Control != null)
            {
                SetKerning();
            }
        }

        private void SetKerning()
        {
            var element = Element as KerningLabel;
            if (element != null && Control != null)
            {
                Control.Text = element.Text;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    Control.LetterSpacing = (float)((element.Kerning) / 10.0f);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == KerningLabel.KerningProperty.PropertyName ||
                e.PropertyName == KerningLabel.TextProperty.PropertyName)
            {
                SetKerning();
            }
        }
    }
}