using MainUser.Views.Medicine;
using MainUser.Views.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.CaretakerPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPatient : ContentPage
    {
        public MenuPatient(UserTypeModel userTypeModel)
        {
            InitializeComponent();
            string email = userTypeModel.email;
            Preferences.Set("PatientEmail", email);
            LabelPatientName.Text = userTypeModel.fullName;
            //LabelPatientEmail.Text = email;
            if (userTypeModel.profileImage != null)
            {
                profileImage.Source = userTypeModel.profileImage;
            }
        }

        private void BtnReminder_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ReminderListPage());
        }

        private void BtnMedicine_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MedicineListPage());
        }

        private void BtnMedicineHistory_Clicked(object sender, EventArgs e)
        {

        }
    }
}