using System;
using TwitterSafari.Models;
using Xamarin.Forms;

namespace TwitterSafari
{
    public partial class MainPage : ContentPage
    {
        private static readonly TwitterViewModel _twitterViewModel = ServiceContainer.Resolve<TwitterViewModel>();
        private bool _isRunning = false;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            if(Parent == null)
            {
                BindingContext = null;
            }
            else
            {
                BindingContext = _twitterViewModel;
            }
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                var searchText = _searchEntry.Text.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    _twitterViewModel.Tweets = null;
                    _backgroundImage.IsVisible = true;
                    return;
                }
                   

                await _twitterViewModel.Search(searchText);
                _backgroundImage.IsVisible = false;
            }
            finally
            {
                _isRunning = false;
            }
        }

        private async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                var tweet = e.Item as Tweet;
                if (tweet != null)
                {
                    _twitterViewModel.CurrentUser = tweet.User;
                    await _twitterViewModel.SearchUserTweets();
                    await Navigation.PushAsync(new UserStatusPage());
                }
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}