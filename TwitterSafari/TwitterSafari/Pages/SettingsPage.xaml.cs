using System;
using TwitterSafari.Models;
using Xamarin.Forms;

namespace TwitterSafari.Pages
{
    public partial class SettingsPage : ContentPage
    {
        static readonly SettingsPageViewModel _settingsPageViewModel = ServiceContainer.Resolve<SettingsPageViewModel>();
        bool _isRunning;

        public SettingsPage()
        {
            InitializeComponent();

            _entry1.Unfocused += (sender, e) =>
            {
                _settingsPageViewModel.SaveSettings();
            };
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
                BindingContext = _settingsPageViewModel;
            }
        }

        async void CloseClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                await Navigation.PopModalAsync();
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}