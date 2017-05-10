using System;
using TwitterSafari.Models;
using Xamarin.Forms;

namespace TwitterSafari
{
    public partial class MainPage : ContentPage
    {
        private static readonly TwitterViewModel _twitterViewModel = ServiceContainer.Resolve<TwitterViewModel>();
        private bool _isSearching = false;

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
            if (_isSearching)
                return;

            _isSearching = true;

            try
            {
                var searchText = _searchEntry.Text;
                if (string.IsNullOrEmpty(searchText))
                    return;

                await _twitterViewModel.Search(searchText);
            }
            finally
            {
                _isSearching = false;
            }
        }
    }
}