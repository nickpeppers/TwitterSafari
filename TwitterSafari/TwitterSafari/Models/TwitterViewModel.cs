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
        private const int TweetCount = 100;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Status> _tweets;

        public ObservableCollection<Status> Tweets
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

        private User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser == value)
                    return;

                _currentUser = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Status> _userStatus;

        public ObservableCollection<Status> UserStatus
        {
            get { return _userStatus; }
            set
            {
                if (_userStatus == value)
                    return;

                _userStatus = value;
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
                      search.Query == searchText &&
                      search.Count == TweetCount
                select search)
               .SingleAsync();

            Tweets = new ObservableCollection<Status>(searchResponse.Statuses);
        }

        public async Task GetUserTweets()
        {
            if (CurrentUser == null)
                throw new Exception("Oops, your CurrentUser is null");

            var userStatus = await
               (from tweet in _twitterContext.Status
                where tweet.Type == StatusType.User &&
                      tweet.ScreenName == CurrentUser.ScreenNameResponse &&
                      tweet.Count == TweetCount
                select tweet).ToListAsync();

            UserStatus = new ObservableCollection<Status>(userStatus);
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