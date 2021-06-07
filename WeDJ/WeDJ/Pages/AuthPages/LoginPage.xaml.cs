using KompiMe.Views;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeDJ.AuthPages;
using WeDJ.Controls;
using WeDJ.Models;
using WeDJ.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeDJ.Pages.AuthPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public bool preventTimer = false;
        public bool showPassword = false;
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //BackgroundImage = "bg.png";


            if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
            {
                BackArrow.Margin = new Thickness(0, 10, 0, 0);
                LoginLbl.Margin = new Thickness(0, 10, 0, 0);

                PasswordEntry.Margin = new Thickness(-5, 15, 0, -15);
                EmailEntry.Margin = new Thickness(-5, 15, 0, -15);


            }
            else
            {

                PasswordEntry.Margin = new Thickness(-5, 35, 0, -35);
                EmailEntry.Margin = new Thickness(-5, 35, 0, -35);

            }

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                LoginLbl.FontSize = 22;
                Title.FontSize = 38;

                PasswordLabel.FontSize = 20;
                EmailLabel.FontSize = 20;
                PasswordEntry.FontSize = 34;
                EmailEntry.FontSize = 34;
                ErrorTypeMsg.FontSize = 18;
                ErrorMsg.FontSize = 18;

            }
            else
            {
                LoginLbl.FontSize = 16;
                LoginBtn.FontSize = 14;
                ErrorTypeMsg.FontSize = 12;
                ErrorMsg.FontSize = 12;

            }

            EmailEntry.Completed += (s, e) =>
            {
                LoginClick(s, e);
            };

            PasswordEntry.Completed += (s, e) =>
            {
                LoginClick(s, e);
            };

            EmailEntry.Unfocused += (s, e) =>
            {
                if (EmailEntry.Text != null)
                {
                    if (!IsValidEmailAddress(EmailEntry.Text))
                    {
                        if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                        {
                            MainPageGrid.RowDefinitions[0].Height = 40;
                        }
                        else
                        {
                            MainPageGrid.RowDefinitions[0].Height = 30;
                        }

                        preventTimer = true;

                        ErrorTypeMsg.Text = "Error -";
                        ErrorMsg.Text = "Invalid Email Address";
                    }
                    else
                    {
                        preventTimer = false;

                    }
                }
            };
        }


        private void ShowPassword(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                if (showPassword)
                {

                    ShowLabel.Text = "Show";
                    showPassword = false;
                    PasswordEntry.IsPassword = true;

                }
                else
                {
                    ShowLabel.Text = "Hide";
                    showPassword = true;
                    PasswordEntry.IsPassword = false;

                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var transitionType = TransitionType.SlideFromLeft;
                var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;

                    if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                    {
                        Navigation.PopAsync();
                    }
                    else
                    {
                        Navigation.PushAsync(new MainPage());
                    }
                }
            }
            return true;
        }
        private async void BackToMain(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var transitionType = TransitionType.SlideFromLeft;
                var transitionNavigationPage = Parent as Controls.TransitionNavigationPage;

                if (transitionNavigationPage != null)
                {
                    transitionNavigationPage.TransitionType = transitionType;

                    if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                    {
                        Navigation.PopAsync();
                    }
                    else
                    {
                        Navigation.PushAsync(new MainPage());
                    }

                }
            }
        }

        private async void ForgotPassword(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PushAsync(new ForgotPasswordPage());
            }
        }

        private async void LoginClick(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (EmailEntry.Text == null || EmailEntry.Text == "")
                {
                    EmailEntry.Focus();
                }
                else if (PasswordEntry.Text == null || PasswordEntry.Text == "")
                {
                    PasswordEntry.Focus();
                }
                else if (!IsValidEmailAddress(EmailEntry.Text))
                {
                    if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                    {
                        MainPageGrid.RowDefinitions[0].Height = 40;
                    }
                    else
                    {
                        MainPageGrid.RowDefinitions[0].Height = 30;
                    }

                    preventTimer = true;
                    ErrorTypeMsg.Text = "Error -";
                    ErrorMsg.Text = "Invalid Email Address";
                }
                else
                {
                    LoginBtn.IsVisible = false;
                    LoginLoader.IsVisible = true;
                    UserServices us = new UserServices();
                    User user = new User();
                    user.Email = EmailEntry.Text;
                    user.Password = PasswordEntry.Text;

                    ApiResponse response = await us.Login(user);

                    //ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(loginResponse.ToString());

                    if (response.HasError)
                    {
                        if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                        {
                            MainPageGrid.RowDefinitions[0].Height = 40;
                        }
                        else
                        {
                            MainPageGrid.RowDefinitions[0].Height = 30;
                        }

                        preventTimer = true;
                        ErrorTypeMsg.Text = "Error -";
                        ErrorMsg.Text = "Invalid Login Credentials";

                        LoginBtn.IsVisible = true;
                        LoginLoader.IsVisible = false;

                    }
                    else {
                        Application.Current.MainPage = new MenuPage(new LocationsPage());
                        //await Navigation.PushAsync();
                    }

                }

                //else if
                //{
                //   
                //}
            }


        }

        public static bool IsValidEmailAddress(string emailaddress)
        {
            try
            {
                Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(emailaddress);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void FocusPassword(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Task.Delay(100);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    PasswordEntry.Focus();

                });
            }

        }

        private void FocusEmail(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Task.Delay(100);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    EmailEntry.Focus();

                });
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
                else if (!preventTimer)
                {
                    MainPageGrid.RowDefinitions[0].Height = 0;

                }



                return shouldTimerContinueWork;
            });



        }


    }
}