using KompiMe.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.FacebookClient;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeDJ.AuthPages;
using WeDJ.Controls;
using WeDJ.Helpers;
using WeDJ.Models;
using WeDJ.Pages;
using WeDJ.Pages.AuthPages;
using WeDJ.Services;
using Xamarin.Forms;

namespace WeDJ
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                LoginLbl.FontSize = 22;
                ToSlbl.FontSize = 24;
                ErrorTypeMsg.FontSize = 18;
                ErrorMsg.FontSize = 18;
            }
            else
            {
                LoginLbl.FontSize = 16;
                ToSlbl.FontSize = 16;
                ErrorTypeMsg.FontSize = 12;
                ErrorMsg.FontSize = 12;
            }
            if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
            {
                LoginLbl.Margin = new Thickness(0, 10, 0, 0);
                CompanyIcon.Margin = new Thickness(20, 2, 0, 0);
            } 
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                var shouldTimerContinueWork = true;

                if (!CrossConnectivity.Current.IsConnected)
                {
                    if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                    {
                        MainPageGrid.RowDefinitions[0].Height = 40;
                    }
                    else
                    {
                        MainPageGrid.RowDefinitions[0].Height = 30;
                    }
                    ErrorTypeMsg.Text = "Error -";
                    ErrorMsg.Text = "Please connect to internet and try again";
                }
                else
                {
                    MainPageGrid.RowDefinitions[0].Height = 0;
                }
                return shouldTimerContinueWork;
            });
        }


        private async void SubmitFbLogin(object sender, EventArgs e)
        {          
            if (CrossConnectivity.Current.IsConnected)
            {
                FacebookButton.IsVisible = false;
                FacebookLoader.IsVisible = true;
                var x = await CrossFacebookClient.Current.RequestUserDataAsync(new string[] { "email", "first_name", "last_name", "gender", "picture" }, new string[] { "email", "user_friends" });
                
                if (x.Data != null)
                {
                    var fbUserID = x.Data["user_id"];
                    var fbToken = x.Data["token"];
                    var xx = "https://graph.facebook.com/v2.3/" + fbUserID + "/user_friends?access_token=" + fbToken;
                    var fbEmail = x.Data["email"];
                    var fbFirstName = x.Data["first_name"];
                    var fbLastName = x.Data["last_name"];
                    var fbProfilePicture = "https://graph.facebook.com/" + fbUserID + "/picture?type=large";
                    var fbBirthday = new DateTime();

                    FacebookLogin fbLogin = new FacebookLogin();
                    fbLogin.FacebookUserID = fbUserID.ToString();
                    fbLogin.FacebookAccessToken = fbToken.ToString();

                    FacebookServices fs = new FacebookServices();
                    FacebookRegister fbRegister = new FacebookRegister();

                    fbRegister.FacebookUserID = fbUserID.ToString();
                    fbRegister.FacebookAccessToken = fbToken.ToString();
                    fbRegister.LastName = fbLastName.ToString();
                    fbRegister.FirstName = fbFirstName.ToString();
                    fbRegister.ProfilePicture = fbProfilePicture.ToString();
                    fbRegister.BirthDate = null;
                    fbRegister.Email = fbEmail.ToString();
                    fbRegister.Phone = string.Empty;

                    var fbRegisterJson = await fs.FacebookRegister(fbRegister);

                    AppSession appSession = JsonConvert.DeserializeObject<AppSession>(fbRegisterJson.ToString());
                    Settings.SessionToken = appSession.SessionToken;
                    Settings.UserID = appSession.User.UserID;
                    Settings.UserFirstName = fbRegister.FirstName;
                    Settings.UserPicture = "https://graph.facebook.com/" + fbUserID + "/picture?type=large"; 
                    Settings.UserPicture = "https://graph.facebook.com/" + fbUserID + "/picture?type=large";

                    var masterMenuPage = new MenuPage(new LocationsPage());
                    Application.Current.MainPage = masterMenuPage;

                }
                else 
                {
                    FacebookButton.IsVisible = true;
                    FacebookLoader.IsVisible = false;
                }
              

            }
        }
        private async void partyBtn(object sender, EventArgs e)
        {
        }

        private async void ForgotRedirect(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var transitionType = TransitionType.SlideFromLeft;
                var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;
                    await Navigation.PushAsync(new ForgotPasswordPage());
                }
            }
        }

        private async void CreateAccount(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var transitionType = TransitionType.SlideFromRight;
                var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;
                    await Navigation.PushAsync(new RegisterPage());
                }
            }
        }

        private async void LoginPage(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var transitionType = TransitionType.SlideFromRight;
                var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;
                    await Navigation.PushAsync(new LoginPage());
                }
            }
        }


        private async void TermsRedirect(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var transitionType = TransitionType.SlideFromBottom;
                var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;
                    await Navigation.PushAsync(new WebviewPage("http://wedj.me/privacy-policy.html"));
                }
            }
        }
       
    }
}
