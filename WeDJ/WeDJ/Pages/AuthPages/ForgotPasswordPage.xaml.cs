using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeDJ.Controls;
using WeDJ.Models;
using WeDJ.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeDJ.AuthPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        public bool preventTimer = false;

        public ForgotPasswordPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);




            if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
            {
                BackArrow.Margin = new Thickness(0, 10, 0, 0);
                //LoginLbl.Margin = new Thickness(0, 10, 0, 0);
                EmailEntry.Margin = new Thickness(-5, 15, 0, -15);


            }
            else
            {

                EmailEntry.Margin = new Thickness(-5, 35, 0, -35);

            }

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                //LoginLbl.FontSize = 22;

                EmailLbl.FontSize = 20;
                EmailEntry.FontSize = 34;
                ForgotPassLbl.FontSize = 38;
                ErrorTypeMsg.FontSize = 18;
                ErrorMsg.FontSize = 18;

            }
            else
            {
                //LoginLbl.FontSize = 16;
                ErrorTypeMsg.FontSize = 12;
                ErrorMsg.FontSize = 12;

            }

            EmailEntry.Completed += (s, e) =>
            {
                ResetPassword(s, e);
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

        private async void ResetPassword(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                if (EmailEntry.Text == null || EmailEntry.Text == "")
                {
                    EmailEntry.Focus();
                }
                else
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


                        User user = new User();
                        user.Email = EmailEntry.Text;
                        UserServices us = new UserServices();
                        ApiResponse response = await us.ForgotPassword(user);

                        var xxx = response.Response.ToString();
                        if (xxx == "True")
                        {

                        }
                        else
                        {
                            preventTimer = true;

                            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                            {
                                MainPageGrid.RowDefinitions[0].Height = 40;
                            }
                            else
                            {
                                MainPageGrid.RowDefinitions[0].Height = 30;
                            }

                            ErrorTypeMsg.Text = "Error -";
                            ErrorMsg.Text = "Email Address Not Found";
                        }
                    }
                }
            }
        }


        private async void LoginRedirect(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                await Navigation.PopToRootAsync();
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