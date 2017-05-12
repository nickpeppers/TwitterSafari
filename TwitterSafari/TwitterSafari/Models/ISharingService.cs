using LinqToTwitter;

namespace TwitterSafari.Models
{
    public interface ISharingService
    {
        void ShareTweet(Status status);
    }
}