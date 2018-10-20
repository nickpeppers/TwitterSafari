using LinqToTwitter;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TwitterSafari.Models
{
    public class TwitterViewModel : INotifyPropertyChanged
    {
        static readonly ISettings _settings = ServiceContainer.Resolve<ISettings>();

        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Status> _tweets;

        public ObservableCollection<Status> Tweets
        {
            get { return _tweets; }
            set
            {
                if (_tweets == value)
                    return;

                _tweets = value;
                OnPropertyChanged(nameof(Tweets));
            }
        }

        User _currentUser;

        public User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser == value)
                    return;

                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        ObservableCollection<Status> _userStatus;

        public ObservableCollection<Status> UserStatus
        {
            get { return _userStatus; }
            set
            {
                if (_userStatus == value)
                    return;

                _userStatus = value;
                OnPropertyChanged(nameof(UserStatus));
            }
        }

        ApplicationOnlyAuthorizer _auth;
        TwitterContext _twitterContext;

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
            try
            {
                Search searchResponse = await
               (from search in _twitterContext.Search
                where search.Type == SearchType.Search &&
                      search.Query == searchText &&
                      search.Count == _settings.TweetCount
                select search)
               .SingleAsync();

                Tweets = new ObservableCollection<Status>(searchResponse.Statuses);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error: " + exc);
            }
        }

        public async Task GetUserTweets()
        {
            try
            {
                if (CurrentUser == null)
                    throw new Exception("Oops, your CurrentUser is null");

                var userStatus = await
                   (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.ScreenName == CurrentUser.ScreenNameResponse &&
                          tweet.Count == _settings.TweetCount
                    select tweet).ToListAsync();

                UserStatus = new ObservableCollection<Status>(userStatus);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("Error: " + exc);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}