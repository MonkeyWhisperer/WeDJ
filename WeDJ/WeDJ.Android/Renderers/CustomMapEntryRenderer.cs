
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
using Android.Runtime;

[assembly: ExportRenderer(typeof(CustomMapEntry), typeof(CustomMapEntryRenderer))]

namespace WeDJ.Droid.Renderers
{
    public class CustomMapEntryRenderer : EntryRenderer
    {
        public CustomMapEntryRenderer(Context context) : base(context)
        {
        }

        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.Background = this.Resources.GetDrawable(Resource.Drawable.ImageEntry);
                this.Control.SetHintTextColor(global::Android.Graphics.Color.Rgb(0,0, 0));
                this.Control.SetTextColor(global::Android.Graphics.Color.Rgb(0,0, 0));
                this.Control.Gravity = GravityFlags.CenterVertical;
                this.Control.SetPadding(30, 0, 0, 0);
            }
        }
    }
}