using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using CustomRenderer;
using CustomRenderer.iOS;
using WeDJ.CustomRenderers;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace CustomRenderer.iOS
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Control != null)
            { 
                if (Control.TitleLabel.Text == "Continue with Facebook")
                {
                    Control.Layer.BackgroundColor = UIColor.White.CGColor;
                }

                Control.Font = UIFont.FromName("CircularStd-Medium", 16);

                if (Control.TitleLabel.Text == "Continue with Facebook" || Control.TitleLabel.Text == "Create Account")
                {
                    if (UIScreen.MainScreen.Bounds.Height == 568)
                    {
                        Control.Layer.CornerRadius = 23;
                    }
                    else if (UIScreen.MainScreen.Bounds.Height == 667)
                    {
                        Control.Layer.CornerRadius = 27;

                    }
                }
                else if (Control.TitleLabel.Text.Contains("PARTY")  )
                {
                    Control.Layer.CornerRadius = 0;

                }
                else if ( Control.TitleLabel.Text == "Try Again")
                {
                    Control.Layer.CornerRadius = 0;
                    Control.ContentEdgeInsets = new UIEdgeInsets(0, 10, 0, 10);


                }
                else if (Control.TitleLabel.Text == "Add Song")
                    {
                    Control.Layer.CornerRadius = 0;
                    Control.ContentEdgeInsets = new UIEdgeInsets(0, 10, 0, 10);
                    
                    Control.Font = UIFont.FromName("CircularStd-Medium", 12);

                }
                else
                {
                    if (UIScreen.MainScreen.Bounds.Height == 568)
                    {
                        Control.Layer.CornerRadius = 17;
                    }
                    else if (UIScreen.MainScreen.Bounds.Height == 667)
                    {
                        Control.Layer.CornerRadius = 21;

                    }
                }
                Control.Layer.BorderWidth = 2;
                Control.Layer.BorderColor = UIColor.White.CGColor; 
            }
        }
    }
}

