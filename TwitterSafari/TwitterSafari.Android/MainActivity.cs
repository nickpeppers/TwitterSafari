using Android.App;
using Android.Content.PM;
using Android.OS;
using TwitterSafari.Droid.Services;
using TwitterSafari.Models;
using Xamarin.Forms.Platform.Android;

namespace TwitterSafari.Droid
{
    [Activity(Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ServiceContainer.Register<ISharingService>(() => new SharingService(this));
            ServiceContainer.Register<ISettings>(() => new AndroidSettings(this));
            ServiceContainer.Register(() => new TwitterViewModel());
            ServiceContainer.Register(() => new SettingsPageViewModel());

            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}