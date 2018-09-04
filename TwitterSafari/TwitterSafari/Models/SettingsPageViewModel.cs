using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TwitterSafari.Models
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        static readonly ISettings _settings = ServiceContainer.Resolve<ISettings>();

        public event PropertyChangedEventHandler PropertyChanged;

        int _tweetCount = _settings.TweetCount == 0 ? 100 : _settings.TweetCount;

        public int TweetCount
        {
            get { return _tweetCount; }
            set
            {
                _tweetCount = value;
                OnPropertyChanged(nameof(TweetCount));
            }
        }

        public void SaveSettings()
        {
            if (TweetCount <= 0)
            {
                TweetCount = 1;
            }
            _settings.TweetCount = TweetCount;
            _settings.Save();
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}