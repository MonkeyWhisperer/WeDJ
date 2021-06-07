//using IconMobileApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDJ.Helpers;

using Xamarin.Forms;
using KompiMe.Views;
using WeDJ.Models;
using Newtonsoft.Json;
using WeDJ.Services;
using WeDJ.Pages;

namespace KompiMe.Views
{
    public partial class MenuPage : MasterDetailPage
    {
        public MenuPage(Page page = null)
        {


            InitializeComponent();

            FirstName.Text = Settings.UserFirstName;

            if (page == null)

                page = new LocationsPage();



            Detail = new NavigationPage(page) { BarTextColor = Color.White };

            if (page.Title == null)
            {
                //MapIcon.IconColor = Color.White;
                //MapLink.TextColor = Color.White;
                MapLink.Opacity = 1;
                



                if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                {
                    MapLink.FontFamily = "CircularStd-Bold";
                }
                else
                { 
                    MapLink.FontFamily = "CircularStd-Bold.otf#CircularStd-Bold";
                }
                MessagingCenter.Subscribe<LocationsPage>(this, "OpenMenu", (sender) => {
                    IsPresented = true;
                });
            }

            if (page.Title == "Playlist")
            {
                //ProfileIcon.IconColor = Color.White;
                //ProfileLink.TextColor = Color.White;
                //ProfileLink.Opacity = 1;
                MessagingCenter.Subscribe<PlaylistPage>(this, "OpenMenu2", (sender) => {
                    IsPresented = true;
                });
            }

            if (page.Title == "View Playlist")
            {
                //ProfileIcon.IconColor = Color.White;
                //ProfileLink.TextColor = Color.White;
                //ProfileLink.Opacity = 1;
                MessagingCenter.Subscribe<ViewPlaylistPage>(this, "OpenMenu3", (sender) => {
                    IsPresented = true;
                });
            }

            //if (page.Title == "WeDJ Events")
            //{
            //    EventsIcon.IconColor = Color.White;
            //    EventsLink.TextColor = Color.White;
            //}
            //if (page.Title == "Company")
            //{
            //    CompanyIcon.IconColor = Color.White;
            //    CompanyLink.TextColor = Color.White;
            //}
            //if (page.Title == "Colleagues")
            //{
            //    ContactsIcon.IconColor = Color.White;
            //    ContactsLink.TextColor = Color.White;
            //}


            //if (Settings.UserPicture == "")
            //{
            //    var imageName = new Label { Text = Settings.UserFullname[0].ToString(), FontSize = 60, FontAttributes = FontAttributes.Bold, TextColor = Color.White, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };
            //    pimage.Children.Add(imageName, 0, 0);
            //}
            ProfileImage.Source = Settings.UserPicture;

         
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    MessagingCenter.Unsubscribe<LocationsPage>(this, "OpenMenu");

        //    MessagingCenter.Unsubscribe<PlaylistPage>(this, "OpenMenu2");
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();

       

   

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Logout(object sender, EventArgs e)
        {
            Settings.SessionToken = "";
            //await Navigation.PushAsync(new MainPage());
        }

        private async void Account(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var masterMenuPage = new MenuPage(new AccountPage());
                    Application.Current.MainPage = masterMenuPage;
                }
                );
            });
        }
        private async void CloseMenu(object sender, EventArgs e)
        {
            IsPresented = false;
        }

        private async void Contacts(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread( () =>
                {
                    //var masterMenuPage = new MenuPage(new TabbedContactsPage());
                    //Application.Current.MainPage = masterMenuPage;
                }
                );
            });
        }

        private async void Posts(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    //var masterMenuPage = new MenuPage(new PostsPage());
                    //Application.Current.MainPage = masterMenuPage;
                }
                );
            });
        }
        private async void Documents(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
               {
                   //var masterMenuPage = new MenuPage(new DocumentsPage());
                   //Application.Current.MainPage = masterMenuPage;
               }
                );
            });
        }


        private async void Announcements(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    //var masterMenuPage = new MenuPage(new AnnouncementsPage());
                    //Application.Current.MainPage = masterMenuPage;
                }
                );
            });
        }

        private async void Events(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    var masterMenuPage = new MenuPage(new TabbedEventsPage());
                    Application.Current.MainPage = masterMenuPage;
                }
                );
            });
        }

        private async void Host(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    //var masterMenuPage = new MenuPage(new CompanyAccountPage());
                    //Application.Current.MainPage = masterMenuPage;
                }
                );
            });
        }

    }
}