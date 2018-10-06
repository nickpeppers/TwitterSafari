using TwitterSafari.Models;

namespace TwitterSafari.UWP.Services
{
    public class WindowsSettings : ISettings
    {
        public int TweetCount { get; set; } = 100;

        public void Save()
        {

        }
    }
}