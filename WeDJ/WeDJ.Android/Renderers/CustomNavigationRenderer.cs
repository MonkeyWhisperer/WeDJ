using System.ComponentModel;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using WeDJ.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NavigationRenderer = Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer;
using Support = Android.Support.V7.Widget;
using Android.Views;
using Android.Support.V4.Content;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRenderer))]

namespace WeDJ.Droid.Renderers
{
    public class NavigationPageRenderer : NavigationRenderer
    {
        private Support.Toolbar toolbar;

        protected override void OnElementChanged(ElementChangedEventArgs<NavigationPage> e)
        {
            base.OnElementChanged(e);
            //toolbar.SetBackgroundColor(this.Resources.GetColor(Resource.Color.primary));   
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);

            if (child.GetType() == typeof(Support.Toolbar))
                toolbar = (Support.Toolbar)child;
        }
    }
}
