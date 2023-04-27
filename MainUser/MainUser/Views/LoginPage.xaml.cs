using MainUser.Views.Flyout;
using MainUser.Views.FlyoutCaretaker;
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
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public LoginPage()
        {
            InitializeComponent();
            bool haskey = Preferences.ContainsKey("token");
            if (haskey)
            {
                string token = Preferences.Get("token", "");
                if (!string.IsNullOrEmpty(token))
                {
                    startup();
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
                    startup();
                }
                else
                {
                    await DisplayAlert("Sign In", "Sign in failed", "Ok");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("Unauthorized", "Email not found", "Ok");
                    return;
                }
                else if (ex.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("Unauthorized", "Invalid password", "Ok");
                    return;
                }
                else
                {
                    await DisplayAlert("Error", ex.Message, "Ok");
                    return;
                }
            }
        }

        private async void ForgetTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgetPasswordPage());
        }

        private async void startup()
        {
            var usertype = await userTypeRepository.GetByEmail();
            string userType = usertype.userType;
            string userfullName = usertype.fullName;
            if (!string.IsNullOrEmpty(userType))
            {
                Preferences.Set("fullName", userfullName);

                if (userType == "Normal User/patient")
                {
                    await Navigation.PushModalAsync(new TabbedPageUser());
                }
                else if (userType == "Family member/caretaker")
                {
                    await Navigation.PushModalAsync(new TabbedPageCaretaker());
                }
            }
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterUser());
        }
    }
}