using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using TwitterSafari;
using TwitterSafari.Droid;
using Android.Text;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]
namespace TwitterSafari.Droid
{
    public class HtmlLabelRenderer : ViewRenderer<Label, TextView>
    {
        TextView _textView;

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            if(Control == null)
            {
                _textView = new TextView(Context);
                SetHtmlText(Element.Text);
                SetNativeControl(_textView);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || Control == null)
                return;

            if (e.PropertyName == HtmlLabel.TextProperty.PropertyName)
            {
                SetHtmlText(Element.Text);
            }
        }

        private void SetHtmlText(string text)
        {
            
            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.N)
            {
                _textView.TextFormatted = Html.FromHtml(text, Android.Text.FromHtmlOptions.ModeCompact);
            }
            else
            {
                _textView.TextFormatted = Html.FromHtml(text);
            }
        }
    }
}