using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Plugin.FacebookClient;
using UIKit;

namespace WeDJ.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            ImageCircleRenderer.Init();
            FormsPlugin.Iconize.iOS.IconControls.Init();
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule())
                       .With(new Plugin.Iconize.Fonts.MaterialModule());
            Xamarin.FormsGoogleMaps.Init("AIzaSyB1F6XjdzuJ45b6U8jNDgX8ZOgPcUm5nRI");
            FacebookClientManager.Initialize(app, options);
            CachedImageRenderer.Init();

            XFGloss.iOS.Library.Init();

            LoadApplication(new App());
            UIApplication.SharedApplication.StatusBarHidden = true;
            return base.FinishedLaunching(app, options);
        }

        public override void OnActivated(UIApplication uiApplication)
        {
            FacebookClientManager.OnActivated();
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return FacebookClientManager.OpenUrl(app, url, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);
        }

        public bool RestrictRotation = true;

        [Export("application:supportedInterfaceOrientationsForWindow:")]
        public UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, IntPtr forWindow)
        {
            if (this.RestrictRotation)
                return UIInterfaceOrientationMask.Portrait;
            else
                return UIInterfaceOrientationMask.AllButUpsideDown;
        }
    }
}
