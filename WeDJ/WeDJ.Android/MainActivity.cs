using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Plugin.CurrentActivity;
using ImageCircle.Forms.Plugin.Droid;
using Xamarin.Forms.GoogleMaps;
using Plugin.FacebookClient;
using Android.Content;
using FFImageLoading.Forms.Droid;

namespace WeDJ.Droid
{
    [Activity(Label = "WeDJ", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            FacebookClientManager.Initialize(this);
            CrossCurrentActivity.Current.Activity = this;
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule())
                        .With(new Plugin.Iconize.Fonts.MaterialModule());
            global::Xamarin.Forms.Forms.Init(this, bundle);

            CachedImageRenderer.Init(true);

            Xamarin.FormsGoogleMaps.Init(this, bundle);
            LoadApplication(new  App());


            XFGloss.Droid.Library.Init(this, bundle);

            ImageCircleRenderer.Init();           
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            FacebookClientManager.OnActivityResult(requestCode, resultCode, intent);
        }
    }
}

