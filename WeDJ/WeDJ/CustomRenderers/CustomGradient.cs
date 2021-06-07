using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform;

namespace WeDJ.CustomRenderers
{
    public class GradientColorStack : StackLayout
    {
        public Color StartColor
        {
            get;
            set;
        }
        public Color EndColor
        {
            get;
            set;
        }
    }
}