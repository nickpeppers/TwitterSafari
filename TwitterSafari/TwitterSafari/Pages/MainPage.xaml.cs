using System;
using System.Threading.Tasks;
using TwitterSafari.Models;
using TwitterSafari.Pages;
using Xamarin.Forms;

namespace TwitterSafari
{
    public partial class MainPage : ContentPage
    {
        static readonly TwitterViewModel _twitterViewModel = ServiceContainer.Resolve<TwitterViewModel>();
        bool _isRunning = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent == null)
            {
                BindingContext = null;
            }
            else
            {
                BindingContext = _twitterViewModel;
            }
        }

        async void OnSearchClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                var searchText = _searchEntry.Text.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    _listView.SelectedItem =
                        _twitterViewModel.Tweets = null;
                    _backgroundImage.IsVisible = true;
                    return;
                }

                await Task.Run(async () =>
                {
                    await _twitterViewModel.Search(searchText);
                });

                if (_twitterViewModel.Tweets != null && _twitterViewModel.Tweets.Count > 0)
                {
                    _backgroundImage.IsVisible = false;
                }
                else
                {
                    _listView.SelectedItem = null;
                    _backgroundImage.IsVisible = true;
                }
            }
            finally
            {
                _isRunning = false;
            }
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                var tweet = e.Item as LinqToTwitter.Status;
                _listView.SelectedItem = null;
                if (tweet != null)
                {
                    _twitterViewModel.CurrentUser = tweet.User;
                    await Task.Run(async () =>
                    {
                        await _twitterViewModel.GetUserTweets();
                    });
                    await Navigation.PushAsync(new UserStatusPage());
                }
            }
            finally
            {
                _isRunning = false;
            }
        }

        async void OnSettingsClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                await Navigation.PushModalAsync(new SettingsPage());
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}