using Android;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using System.Threading.Tasks;

namespace TwitterSafari.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SplashLayout);

            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var iconBitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.Icon);
                var taskDescription = new ActivityManager.TaskDescription("Twitter Safari", iconBitmap, Color.ParseColor("#555555"));
                SetTaskDescription(taskDescription);
            }
        }

        protected async override void OnResume()
        {
            base.OnResume();

            await Task.Delay(2000);

            StartActivity(typeof(MainActivity));
        }
    }
}