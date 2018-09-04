using System;
using Android.Content;
using LinqToTwitter;
using TwitterSafari.Models;

namespace TwitterSafari.Droid.Services
{
    public class SharingService : ISharingService
    {
        Context _context;

        public SharingService(Context context)
        {
            _context = context;
        }

        public void ShareTweet(Status status)
        {
            var textToSend = status.User.Name + Environment.NewLine + status.Text;
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, textToSend);
            _context.StartActivity(Intent.CreateChooser(intent, "Choose an App"));
        }
    }
}