using KompiMe.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeDJ.Helpers;
using WeDJ.Pages;
using Xamarin.Forms;

namespace WeDJ
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //MainPage = new Controls.TransitionNavigationPage(new MainPage());
            if (Settings.SessionToken == "")
            {
                MainPage = new Controls.TransitionNavigationPage(new MainPage());


            }
            else
            {
                //MainPage = new Controls.TransitionNavigationPage(new MenuPage());
                MainPage = new MenuPage();
            }

        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
