using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserTypeRegister : ContentPage
    {
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public UserTypeRegister()
        {
            InitializeComponent();
        }

        private async void ButtonSetUserType_Clicked(object sender, EventArgs e)
        {
            string userType = pickerUserType.SelectedItem as String;
            if (pickerUserType.SelectedIndex == -1)
            {
                await DisplayAlert("Warning", "Please enter user type", "Cancel");
            }

            UserTypeModel userTypeModel = new UserTypeModel();
            userTypeModel.email = Preferences.Get("userEmail", "default");
            userTypeModel.fullName = Preferences.Get("userfullName", "default");
            userTypeModel.userType = userType;

            var isSaved = await userTypeRepository.Save(userTypeModel);
            if (isSaved)
            {
                Preferences.Set("userType", userType);
                Preferences.Set("userEmail", "");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Warning", "Reminder saved fail", "Ok");
            }
        }
    }
}