using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSafari.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TwitterSafari
{
    public partial class UserStatusPage : ContentPage
    {
        private static readonly TwitterViewModel _twitterViewModel = ServiceContainer.Resolve<TwitterViewModel>();
        private bool _isRunning = false;

        public UserStatusPage()
        {
            InitializeComponent();

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += BackTapped;
            _backArrowImage.GestureRecognizers.Add(tapGesture);
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

        private async void BackTapped(object sender, EventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                await Navigation.PopAsync();
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}