using Foundation;
using System.ComponentModel;
using TwitterSafari;
using TwitterSafari.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]
namespace TwitterSafari.iOS
{
    public class HtmlLabelRenderer : ViewRenderer<Label, UITextView>
    {
        UITextView _textView;

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
                return;

            if(Control == null)
            {
                _textView = new UITextView();
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
            var attr = new NSAttributedStringDocumentAttributes { DocumentType = NSDocumentType.HTML };
            var nsError = new NSError();

            _textView.Editable = false;
            _textView.AttributedText = new NSAttributedString(text, attr, ref nsError);
            _textView.DataDetectorTypes = UIDataDetectorType.All;
        }
    }
}