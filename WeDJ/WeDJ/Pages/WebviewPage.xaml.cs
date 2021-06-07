using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeDJ.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebviewPage : ContentPage
    {
        public WebviewPage(string url)
        {
            var browser = new WebView();
            browser.Source = url;
            Content = browser;
            Title = "Find out why & how";
        }
    }
}