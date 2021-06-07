
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WeDJ.Droid.Renderers;
using Android.Support.V4.Content;
using Android.Graphics;
using WeDJ.CustomRenderers;
using Android.Views;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace WeDJ.Droid.Renderers
{
#pragma warning disable CS0618 // Type or member is obsolete
    public class CustomButtonRenderer : ButtonRenderer
    {

        private Bitmap hix;
        // Override the OnElementChanged method so we can tweak this renderer post-initial setup

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                 

                if (this.Control.Text == "Continue with Facebook")
                {
                    this.Control.Background = this.Resources.GetDrawable(Resource.Drawable.ImageButtonFull);

                }
                else
                {
                    Control.Background = Resources.GetDrawable(id: Resource.Drawable.ImageButton);
                }
           
                Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "CircularStd-Medium.otf");
                
                this.Control.Typeface = font;
               

                if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.Control.SetTextSize(Android.Util.ComplexUnitType.Px, 38);

                    if (this.Control.Text == "JOIN PARTY" || this.Control.Text == "Add" || this.Control.Text == "VIEW PARTY")
                    {
                        Control.Background = Resources.GetDrawable(id: Resource.Drawable.ImageButtonSquare);

                        this.Control.SetTextSize(Android.Util.ComplexUnitType.Px, 54);

                    }
                    if (this.Control.Text == "Add Song")
                    {
                        Control.Background = Resources.GetDrawable(id: Resource.Drawable.ImageButtonSquare);
                        this.Control.SetTextSize(Android.Util.ComplexUnitType.Px, 30);
                        Control.SetPadding(25, 24, 25, 26);
                    }


                    if (this.Control.Text == "Continue with Facebook")
                    {
                        //this.Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.facebook, 0, 0, 0);

                    }

                    if (this.Control.Text == "Continue with Facebook" || this.Control.Text == "Create Account")
                    {
                        this.Control.SetTextSize(Android.Util.ComplexUnitType.Px, 55);
                    }
                }
                else
                {

                    if (this.Control.Text == "JOIN PARTY" || this.Control.Text == "Add" || this.Control.Text == "VIEW PARTY")
                    {
                        Control.Background = Resources.GetDrawable(id: Resource.Drawable.ImageButtonSquare);

                        this.Control.SetTextSize(Android.Util.ComplexUnitType.Px, 64);

                    }

                    if (this.Control.Text == "Continue with Facebook")
                    {
                        //this.Control.SetCompoundDrawablesRelativeWithIntrinsicBounds(Resource.Drawable.facebook_tablet, 0, 0, 0);

                    }
                    this.Control.SetTextSize(Android.Util.ComplexUnitType.Px, 60);

                }

                this.Control.SetAllCaps(false);
                this.Control.Gravity = GravityFlags.Center;
            }
        }

    }
#pragma warning restore CS0618 // Type or member is obsolete
}