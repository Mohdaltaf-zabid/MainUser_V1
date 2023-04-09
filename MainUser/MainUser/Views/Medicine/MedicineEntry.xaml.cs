using MainUser.Views.Reminder;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views.Medicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedicineEntry : ContentPage
    {

        MediaFile file;
        MedicineRepository medicineRepository = new MedicineRepository();
        public MedicineEntry()
        {
            InitializeComponent();
        }

        private void TxtStrengthUnit_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushAsync(new StrengthUnitPage());
        }

        private void TxtDoseTime_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushAsync(new DoseTimePage());
        }

        private async void ButtonSaveMed_Clicked(object sender, EventArgs e)
        {
            var isSaved = false;
            string MedName = TxtMedName.Text;
            DateTime startDate = TxtStartdate.Date;
            DateTime endDate = TxtEnddate.Date;
            string strength = TxtStrength.Text;
            string unit = pickerUnit.SelectedItem as String;
            string frequnecy = pickerFrequency.SelectedItem as String;
            string TimeADay = pickerTimeADay.SelectedItem as String;
            string Dose = TxtDose.Text;

            if (string.IsNullOrEmpty(MedName))
            {
                await DisplayAlert("Warning", "Please enter Medicine name", "Cancel");
                return;
            }

            if (file != null)
            {
                string image = await medicineRepository.upload(file.GetStream(), Path.GetFileName(file.Path));
                //medicineModel.Image = image;
                Preferences.Set("Image", image);
            }

            MedicineModel medicineModel = new MedicineModel();

            foreach (DateTime day in EachDay(startDate, endDate))
            {

                medicineModel.med_Name = MedName;
                medicineModel.med_StartDate = startDate;
                medicineModel.med_EndDate = endDate;
                medicineModel.med_Day = day;
                medicineModel.med_Strength = strength;
                medicineModel.med_Unit = unit;
                medicineModel.email = Preferences.Get("userEmail", "default");
                medicineModel.med_status = "Not taken";
                medicineModel.med_Frequency = frequnecy;
                medicineModel.med_TimesADay = TimeADay;
                medicineModel.med_Frequency = frequnecy;
                medicineModel.med_Dose = Dose;


                string file = Preferences.Get("Image", "");
                if (file != null)
                {
                    medicineModel.Image = Preferences.Get("Image", "");
                }
                if (pickerFrequency.SelectedIndex == 0)
                {
                    medicineModel.med_Frequency = pickerFrequency.SelectedItem as String;
                }
                else
                {
                    medicineModel.med_Frequency = pickerFrequency.SelectedItem as String;
                }
                isSaved = await medicineRepository.Save(medicineModel);
            }

            if (isSaved)
            {
                await DisplayAlert("Information", "Medicine succussful save", "Ok");
                await Navigation.PushAsync(new MedicineListPage());
            }
            else
            {
                await DisplayAlert("Warning", "Medicine saved fail", "Ok");
            }
            /*Preferences.Set("MedName", MedName);
            Preferences.Set("startDate", startDate);
            Preferences.Set("endDate", endDate);
            Preferences.Set("strength", strength);
            Preferences.Set("priority", unit);

            /*medicineModel.med_StartDate = ;
            //medicineModel.med_EndDate = ;
            //medicineModel.med_Strength = Preferences.Get("strength", "");
            //medicineModel.med_Unit = Preferences.Get("priority", "");
            //medicineModel.email = Preferences.Get("userEmail", "default");*/
            /*var isSaved = await medicineRepository.Save(medicineModel);
            //if (isSaved)
            //{
            //await DisplayAlert("Information", "Medicine succussful save", "Ok");
            //await Navigation.PushAsync(new DoseTimePage());
            /*Clear();
            //}
            //else
            //{
            //    await DisplayAlert("Warning", "Medicine saved fail", "Ok");
            */
        }

        private async void cameraIcon_Tapped(object sender, EventArgs e)
        {
            //await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });

                if (file == null)
                {
                    return;
                }
                medicineImage.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();

                });

                //var result = await MediaPicker.CapturePhotoAsync();
                //if (result != null)
                //{
                //    var stream = await result.OpenReadAsync();
                //    medicineImage.Source = ImageSource.FromStream(() => stream);
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void pickerFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerFrequency.SelectedIndex != -1)
            {
                string TimeDay = pickerFrequency.SelectedItem as String;
                string daily = "Daily";
                if (String.Equals(TimeDay, daily))
                {
                    lblTimeADay.IsVisible = true;
                    pickerTimeADay.IsVisible = true;
                    pickerTimeADay.SelectedIndex = -1;
                    lblTimeAndDose.IsVisible = true;
                    TxtDose.IsVisible = true;
                }
                else
                {
                    lblTimeADay.IsVisible = false;
                    pickerTimeADay.IsVisible = false;
                    pickerTimeADay.SelectedIndex = -1;
                    lblTimeAndDose.IsVisible = true;
                    TxtDose.IsVisible = true;
                }
            }
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}