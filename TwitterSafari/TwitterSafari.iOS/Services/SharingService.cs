using Foundation;
using LinqToTwitter;
using System;
using TwitterSafari.Models;
using UIKit;

namespace TwitterSafari.iOS.Services
{
    public class SharingService : ISharingService
    {
        public void ShareTweet(Status status)
        {
            var textToSend = status.User.Name + Environment.NewLine + status.Text;
            var activityController = new UIActivityViewController(new NSObject[] { UIActivity.FromObject(textToSend) }, null);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(activityController, true, null);
        }
    }
}