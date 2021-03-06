﻿using System;
using TwitterSafari.Models;
using Xamarin.Forms;

namespace TwitterSafari
{
    public partial class UserStatusPage : ContentPage
    {
        static readonly TwitterViewModel _twitterViewModel = ServiceContainer.Resolve<TwitterViewModel>();
        static readonly ISharingService _sharingService = ServiceContainer.Resolve<ISharingService>();
        bool _isRunning = false;

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

        async void BackTapped(object sender, EventArgs e)
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

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (_isRunning)
                return;

            _isRunning = true;

            try
            {
                var tweet = e.SelectedItem as LinqToTwitter.Status;
                if (tweet != null)
                {
                    _sharingService.ShareTweet(tweet);
                }
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}