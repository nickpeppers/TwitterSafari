﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TwitterSafari
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TwitterSafari.MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}