namespace TwitterSafari.Models
{
    public interface ISettings
    {
        int TweetCount { get; set; }

        void Save();
    }
}