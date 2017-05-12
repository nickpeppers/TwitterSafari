using Android.App;
using Android.Content.PM;
using Android.OS;
using TwitterSafari.Models;
using Xamarin.Forms.Platform.Android;

namespace TwitterSafari.Droid
{
    [Activity(Label = "TwitterSafari", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ServiceContainer.Register<TwitterViewModel>(() => new TwitterViewModel());

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new TwitterSafari.App());
        }
    }
}