using Foundation;
using LinqToTwitter;
using TwitterSafari.Models;
using UIKit;

namespace TwitterSafari.iOS.Services
{
    public class SharingService : ISharingService
    {
        public void ShareTweet(Status status)
        {
            var activityController = new UIActivityViewController(new NSObject[] { UIActivity.FromObject(status.Text) }, null);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(activityController, true, null);
        }
    }
}