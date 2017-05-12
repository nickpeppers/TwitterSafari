using LinqToTwitter;

namespace TwitterSafari.Models
{
    public class Tweet
    {
        public ulong StatusID { get; set; }

        public string Text { get; set; }

        public User User { get; set; }
    }
}