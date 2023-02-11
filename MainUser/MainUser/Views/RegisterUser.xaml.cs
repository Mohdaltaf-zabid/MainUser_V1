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
    public partial class RegisterUser : ContentPage
    {

        UserRepository userRepository = new UserRepository();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string fullName = TxtFullName.Text;
                string email = TxtEmail.Text;
                string gender = TxtGender.Text;
                string birthDate = TxtBirthDate.Text;
                string password = TxtPassword.Text;

                if (String.IsNullOrEmpty(fullName))
                {
                    await DisplayAlert("Warning", "Please enter Full Name", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Warning", "Please enter Email", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(gender))
                {
                    await DisplayAlert("Warning", "Please enter Gender", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(birthDate))
                {
                    await DisplayAlert("Warning", "Please enter Birth Date", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Please enter Password", "Ok");
                    return;
                }

                var IsSave = await userRepository.Register(email, password, fullName);
                if (IsSave)
                {
                    await DisplayAlert("Register user", "Register complete", "Ok");
                    await Navigation.PopModalAsync();
                }
                await DisplayAlert("Register user", "Register failed", "Ok");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Warning", "Email Exist", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", ex.Message, "Ok");
                }
            }
        }
    }
}