
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WeDJ.Droid.Renderers;
using Android.Support.V4.Content;
using Android.Graphics;
using WeDJ.CustomRenderers;
using Android.Views;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace WeDJ.Droid.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {

        private Bitmap hix;
        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.PaintFlags = PaintFlags.UnderlineText;
                this.Control.TextSize = 20f;
                this.Control.SetTextColor(global::Android.Graphics.Color.Rgb(255, 255, 255));
            }
        }
    }
}