using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Permissions;

namespace WeDJ.Droid
{
    [Activity(Label = "WeDJ", Theme = "@style/MyTheme.Splash", Immersive = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}