using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomSliderDemo.iOS;
using Foundation;
using UIKit;
using WeDJ.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSlider), typeof(MySliderRenderer))]
namespace CustomSliderDemo.iOS
{
    public class MySliderRenderer : SliderRenderer
    {
        private CustomSlider view;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Slider> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;

            view = (CustomSlider)Element;
            if (!string.IsNullOrEmpty(view.ThumbImage))
            {
                //Assigns a thumb image to the specified control states.  
                Control.SetThumbImage(UIImage.FromFile(view.ThumbImage), UIControlState.Normal);
            }
            else if (view.ThumbColor != Xamarin.Forms.Color.Default ||
                view.MaxColor != Xamarin.Forms.Color.Default ||
                view.MinColor != Xamarin.Forms.Color.Default)
                // Set Progress bar Thumb color  

            Control.ThumbTintColor = view.ThumbColor.ToUIColor();
            //this is for Minimum Slider line Color  
            Control.MinimumTrackTintColor = view.MinColor.ToUIColor();
            //this is for Maximum Slider line Color  
            Control.MaximumTrackTintColor = view.MaxColor.ToUIColor();

        }
    }
}