using LinqToTwitter;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TwitterSafari.Models
{
    public class TwitterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Tweet> _tweets;

        public ObservableCollection<Tweet> Tweets
        {
            get { return _tweets; }
            set
            {
                if (_tweets == value)
                    return;

                _tweets = value;
                OnPropertyChanged();
            }
        }

        private ApplicationOnlyAuthorizer _auth;
        private TwitterContext _twitterContext;

        public TwitterViewModel()
        {
            InitTweetViewModel();
        }

        public async void InitTweetViewModel()
        {
            _auth = new ApplicationOnlyAuthorizer()
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = "NVnoGfVhW8R6rwCyD7HvB9TPg",
                    ConsumerSecret = "use3WV06gNmoGq9BsW16Odv3DSgJ2GSnB5K88wTYyPouCg31AU",
                },
            };
            await _auth.AuthorizeAsync();

            _twitterContext = new TwitterContext(_auth);
        }

        public async Task Search(string searchText)
        {
            Search searchResponse = await
               (from search in _twitterContext.Search
                where search.Type == SearchType.Search &&
                      search.Query == searchText
                select search)
               .SingleAsync();

            var tweets =(from tweet in searchResponse.Statuses select new Tweet
            {
                StatusID = tweet.StatusID,
                Name = tweet.User.Name,
                Text = tweet.Text,
                Location = tweet.User.Location,
                Followers = tweet.User.FollowersCount,
                ImageUrl = tweet.User.ProfileImageUrl
            });
            
            Tweets = new ObservableCollection<Tweet>(tweets);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException("Can't call OnPropertyChanged with a null property name.", propertyName);

            PropertyChangedEventHandler propChangedHandler = PropertyChanged;
            if (propChangedHandler != null)
                propChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}