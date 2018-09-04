using Foundation;
using TwitterSafari.iOS.Services;
using TwitterSafari.Models;
using UIKit;

namespace TwitterSafari.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate :Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            ServiceContainer.Register<ISharingService>(() => new SharingService());
            ServiceContainer.Register<ISettings>(() => new iOSSettings());
            ServiceContainer.Register(() => new TwitterViewModel());
            ServiceContainer.Register(() => new SettingsPageViewModel());

            Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}