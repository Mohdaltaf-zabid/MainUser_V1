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
    public partial class RegisterUser : ContentPage
    {

        UserRepository userRepository = new UserRepository();
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public RegisterUser()
        {
            InitializeComponent();
            Preferences.Set("userEmail", "");
            Preferences.Set("userfullName", "");
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            try
            {
                string fullName = TxtFullName.Text;
                string email = TxtEmail.Text;
                /*string gender = TxtGender.Text;
                //string birthDate = TxtBirthDate.Text;*/
                string password = TxtPassword.Text;
                DateTime birthdate = TxtBirthDate.Date;
                string userType = pickerUserType.SelectedItem as String;
                string gender = pickerGender.SelectedItem as String;

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
                /*if (String.IsNullOrEmpty(gender))
                //{
                //    await DisplayAlert("Warning", "Please enter Gender", "Ok");
                //    return;
                //}
                //if (String.IsNullOrEmpty(birthDate))
                //{
                //    await DisplayAlert("Warning", "Please enter Birth Date", "Ok");
                //    return;
                }*/
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Warning", "Please enter Password", "Ok");
                    return;
                }
                if (pickerUserType.SelectedIndex == -1)
                {
                    await DisplayAlert("Warning", "Please enter user type", "Ok");
                }if (pickerGender.SelectedIndex == -1)
                {
                    await DisplayAlert("Warning", "Please enter gender", "Ok");
                }

                var IsSave = await userRepository.Register(email, password, fullName);
                if (IsSave)
                {
                    /*Preferences.Set("userEmail", email);
                    //Preferences.Set("userfullName", fullName);
                    await Navigation.PushModalAsync(new UserTypeRegister());*/
                    UserTypeModel userTypeModel = new UserTypeModel();
                    userTypeModel.email = email;
                    userTypeModel.fullName = fullName;
                    userTypeModel.userType = userType;
                    userTypeModel.gender = gender;
                    userTypeModel.birthdate = birthdate;
                    userTypeModel.status = "Add";
                    var isSaved = await userTypeRepository.Save(userTypeModel);
                    if (isSaved)
                    {
                        await DisplayAlert("Register user", "Register complete", "Ok");
                        await Navigation.PopModalAsync();
                    }
                    else
                    {
                        await DisplayAlert("Warning", "Reminder saved fail", "Ok");
                        return;
                    }
                }
                else
                {
                    await DisplayAlert("Register user", "Register failed", "Ok");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Warning", "Email Exist", "Ok");
                    return;
                }
                else
                {
                    await DisplayAlert("Error", ex.Message, "Ok");
                    return;
                }
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}