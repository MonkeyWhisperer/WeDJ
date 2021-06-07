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
	public partial class TabbedEventsPage : TabbedPage
    {
		public TabbedEventsPage ()
		{
			InitializeComponent ();
            Title = "WeDJ Events";
        }
    }
}