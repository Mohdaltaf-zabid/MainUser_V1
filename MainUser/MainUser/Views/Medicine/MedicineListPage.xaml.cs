using MainUser.Views.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views.Medicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicineListPage : ContentPage
    {
        MedicineRepository medicineRepository = new MedicineRepository();
        public MedicineListPage()
        {
            InitializeComponent();
            TxtSearchdate.Date = DateTime.Now;
            MedicineListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            bool haskey = Preferences.ContainsKey("token");
            if (haskey)
            {
                var email = Preferences.Get("PatientEmail", null);
                var date = TxtSearchdate.Date;
                if (!string.IsNullOrEmpty(email))
                {
                    email = Preferences.Get("PatientEmail", "");
                }
                else
                {
                    email = Preferences.Get("userEmail", "");
                }
                var medicine = await medicineRepository.GetMedicineByDate(email, date);
                MedicineListView.ItemsSource = null;
                if (medicine.Count != 0)
                {
                    lblNoRecord.IsVisible = false;
                    MedicineListView.ItemsSource = medicine;
                }
                else
                {
                    lblNoRecord.IsVisible = true;
                }
                MedicineListView.IsRefreshing = false;
            }
        }

        private async void AddMedicineButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MedicineEntry());
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var medcine = await medicineRepository.GetById(id);

            if (medcine == null)
            {
                await DisplayAlert("Warning", "Data not found", "Ok");
                return;
            }
            medcine.ID = id;
            await Navigation.PushModalAsync(new MedUpdateStatus(medcine));
        }

        private void TxtSearchdate_Unfocused(object sender, FocusEventArgs e)
        {
            OnAppearing();
        }

        private void btmHistory_Clicked(object sender, EventArgs e)
        {

        }
    }
}