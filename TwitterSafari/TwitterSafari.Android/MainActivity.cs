﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TwitterSafari.Models;

namespace TwitterSafari.Droid
{
    [Activity(Label = "TwitterSafari", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ServiceContainer.Register<TwitterViewModel>(() => new TwitterViewModel());

            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new TwitterSafari.App());
        }
    }
}

