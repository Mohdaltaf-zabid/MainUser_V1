using MainUser.Views.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserRepository userRepository = new UserRepository();
        public LoginPage()
        {
            InitializeComponent();
            bool haskey = Preferences.ContainsKey("token");
            if (haskey)
            {
                string token = Preferences.Get("token", "");
                if (!string.IsNullOrEmpty(token))
                {
                    Navigation.PushAsync(new TabPage());
                }
            }
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Warning", "Please enter Email", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Please enter password", "Ok");
                    return;
                }
                string token = await userRepository.SignIn(email, password);
                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("token", token);
                    Preferences.Set("userEmail", email);
                    await Navigation.PushAsync(new TabPage());
                }
                else
                {
                    await DisplayAlert("Sign In", "Sign in failed", "Ok");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("Unauthorized", "Email not found", "Ok");
                }
                else if (ex.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("Unauthorized", "Invalid password", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", ex.Message, "Ok");
                }
            }
        }

        private async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterUser());
        }

        private async void ForgetTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgetPasswordPage());
        }
    }
}