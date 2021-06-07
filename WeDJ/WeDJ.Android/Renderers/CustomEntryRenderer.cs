
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WeDJ.Droid.Renderers;
using Android.Support.V4.Content;
using Android.Graphics;
using WeDJ.CustomRenderers;
using Android.Views;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace WeDJ.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e); 
            Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
        }
    }
}