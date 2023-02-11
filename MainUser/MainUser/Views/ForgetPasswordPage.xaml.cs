using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPasswordPage : ContentPage
    {
        UserRepository userRepository = new UserRepository();
        public ForgetPasswordPage()
        {
            InitializeComponent();
        }

        private async void ButtonSendLink_Clicked(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Warning", "Please enter your email.", "Ok");
                return;
            }
            bool IsSend = await userRepository.ResetPassword(email);
            if (IsSend)
            {
                await DisplayAlert("Reset Password", "Send link in your email", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Reset Password", "Link send failed", "Ok");
            }
        }
    }
}