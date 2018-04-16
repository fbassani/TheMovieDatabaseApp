﻿using Android.App;
using Android.Content.PM;
using Android.Net;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace TheMovieDatabaseApp.Android
{
    [Activity(Label = "The Movie Database", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            App.IsNetworkAvailabe = IsNetworkAvailable;
            LoadApplication(new App());
        }

        private bool IsNetworkAvailable()
        {
            var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            return networkInfo?.IsConnected ?? false;
        }
    }
}

