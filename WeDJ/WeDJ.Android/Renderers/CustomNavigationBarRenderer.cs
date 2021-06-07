
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WeDJ.Droid.Renderers;
using Android.Support.V4.Content;
using Android.Graphics;
using WeDJ.CustomRenderers;
using Android.Views;
using Android.App;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(CustomNavigationBar), typeof(CustomNavigationBarRenderer))]

namespace WeDJ.Droid.Renderers
{
    public class CustomNavigationBarRenderer : PageRenderer
    {

        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            //this.SetBackground(Resources.GetDrawable(Resource.Drawable.background));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            // Removing AppIcon from the ActionBar (Xamarin Forms NavigationBar)
            var actionBar = ((Activity)Context).ActionBar;
            //actionBar.SetIcon(new ColorDrawable(Xamarin.Forms.Color.Transparent.ToAndroid()));
        }
    }
}
