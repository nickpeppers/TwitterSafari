using Android.Content;
using LinqToTwitter;
using TwitterSafari.Models;
using Xamarin.Forms;

namespace TwitterSafari.Droid.Services
{
    public class SharingService : ISharingService
    {
        public void ShareTweet(Status status)
        {
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, status.Text);
            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose an App"));
        }
    }
}