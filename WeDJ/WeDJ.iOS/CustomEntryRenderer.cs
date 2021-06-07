
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreAnimation;
using CoreGraphics;
using WeDJ.CustomRenderers;
using WeDJ.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace WeDJ.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {


        //public CustomEntryRendererEdit(Context context) : base(context)
        //{
        //}

        // Override the OnElementChanged method so we can tweak this renderer post-initial setup
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);


            var intHeight = UIScreen.MainScreen.Bounds.Height;
            var intWidth = UIScreen.MainScreen.Bounds.Width;

         

            var borderLayer = new CALayer();
            borderLayer.MasksToBounds = true;
            if (UIScreen.MainScreen.Bounds.Height == 568)
            {

                borderLayer.Frame = new CoreGraphics.CGRect(0f, Frame.Height - 5, 280, 1f);
            }
            else if (UIScreen.MainScreen.Bounds.Height == 667)
            {
                borderLayer.Frame = new CoreGraphics.CGRect(0f, Frame.Height - 5, 325, 1f);

            }
            borderLayer.BorderColor = UIColor.White.CGColor;
            borderLayer.BorderWidth = 1.0f;
            borderLayer.CornerRadius = 1;
            Control.Layer.AddSublayer(borderLayer);
            Control.BorderStyle = UITextBorderStyle.None;
            Control.TintColor = UIColor.White;
            Control.TextColor = UIColor.White;
            Control.TextAlignment = UITextAlignment.Left;
        }

    }
}