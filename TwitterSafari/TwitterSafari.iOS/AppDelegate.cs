using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using TwitterSafari.Models;

namespace TwitterSafari.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate :Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            ServiceContainer.Register<TwitterViewModel>(() => new TwitterViewModel());

            Xamarin.Forms.Forms.Init();
            LoadApplication(new TwitterSafari.App());

            return base.FinishedLaunching(app, options);
        }
    }
}
