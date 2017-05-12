using Android.Content;
using LinqToTwitter;
using System;
using TwitterSafari.Models;
using Xamarin.Forms;

namespace TwitterSafari.Droid.Services
{
    public class SharingService : ISharingService
    {
        public void ShareTweet(Status status)
        {
            var textToSend = status.User.Name + Environment.NewLine + status.Text;
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, textToSend);
            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose an App"));
        }
    }
}