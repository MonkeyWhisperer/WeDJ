using KompiMe.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeDJ.Controls;
using WeDJ.Models;
using WeDJ.Pages.AuthPages;
using WeDJ.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeDJ.AuthPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public bool preventTimer = false;

        public bool showPassword = false;
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            //BackgroundImage = "bg.png";

            EmailEntry.Completed += (s, e) =>
            {
                RegisterClick(s, e);
            };

            NameEntry.Completed += (s, e) =>
            {
                RegisterClick(s, e);
            };

            PasswordEntry.Completed += (s, e) =>
            {
                RegisterClick(s, e);
            };

            if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
            {
                BackArrow.Margin = new Thickness(0, 10, 0, 0);
                LoginLbl.Margin = new Thickness(0, 10, 0, 0);
                NameEntry.Margin = new Thickness(-5, 15, 0, -15);
                EmailEntry.Margin = new Thickness(-5, 15, 0, -15);
                PasswordEntry.Margin = new Thickness(-5, 15, 0, -15);

            }
            else
            {

                NameEntry.Margin = new Thickness(-5, 35, 0, -35);
                EmailEntry.Margin = new Thickness(-5, 35, 0, -35);
                PasswordEntry.Margin = new Thickness(-5, 35, 0, -35);
                ShowLabel.Margin = new Thickness(0, 0, 5, 0);
            }

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                LoginLbl.FontSize = 22;
                Title.FontSize = 38;
                NameLabel.FontSize = 20;
                EmailLabel.FontSize = 20;
                PasswordLabel.FontSize = 20;
                ShowLabel.FontSize = 18;
                NameEntry.FontSize = 34;
                EmailEntry.FontSize = 34;
                PasswordEntry.FontSize = 34;
                ErrorTypeMsg.FontSize = 18;
                ErrorMsg.FontSize = 18;

            }
            else
            {
                LoginLbl.FontSize = 16;
                Title.FontSize = 26;
                ErrorTypeMsg.FontSize = 12;
                ErrorMsg.FontSize = 12;

            }


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

        private async void Login(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                var transitionType = TransitionType.SlideFromRight;
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
                        Navigation.PushAsync(new LoginPage());
                    }
                }
            }
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

        private void FocusName(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                Task.Delay(100);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    NameEntry.Focus();
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
                }  ); 
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

        private async void RegisterClick(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                if (NameEntry.Text == null || NameEntry.Text == "")
                {
                    NameEntry.Focus();
                }

                else if (EmailEntry.Text == null || EmailEntry.Text == "")
                {
                    EmailEntry.Focus();
                }

                else if (PasswordEntry.Text == null || PasswordEntry.Text == "")
                {
                    PasswordEntry.Focus();
                }

                if (NameEntry.Text != null && PasswordEntry.Text != null && EmailEntry.Text != null)
                {
                    if (IsValidEmailAddress(EmailEntry.Text))
                    {
                        RegisterBtn.IsVisible = false;
                        RegisterLoader.IsVisible = true;
                        UserServices us = new UserServices();
                        User user = new User();
                        user.Email = EmailEntry.Text;
                        user.Password = PasswordEntry.Text;
                        user.FirstName = NameEntry.Text;

                        ApiResponse response = await us.CreateAccount(user);

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

                            RegisterBtn.IsVisible = true;
                            RegisterLoader.IsVisible = false;

                            preventTimer = true;
                            ErrorTypeMsg.Text = "Error -";
                            ErrorMsg.Text = "Email Already Used";
                        }
                        else
                        {
                            await Navigation.PushAsync(new MenuPage());


                        }
                    }
                    else
                    {
                        //ValidationMessage.IsVisible = true;

                        //ValidationMessage.Text = "Email invalid";
                    }

                }
                else
                {
                    //ValidationMessage.IsVisible = true;
                    //ValidationMessage.Text = "Fill account details";
                }
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

        private async void LoginRedirect(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PopToRootAsync();
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
                    Navigation.PopAsync();
                }
            }
            return true;

        }


        protected override void OnAppearing()
        {

            if (CrossConnectivity.Current.IsConnected)
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
}