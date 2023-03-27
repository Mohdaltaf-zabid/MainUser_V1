using MainUser.Views.Reminder;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
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
            string MedName = TxtMedName.Text;
            DateTime startDate = TxtStartdate.Date;
            DateTime endDate = TxtEnddate.Date;

            if (string.IsNullOrEmpty(MedName))
            {
                await DisplayAlert("Warning", "Please enter Notes", "Cancel");
            }
            //if (string.IsNullOrEmpty(startDate))
            //{
            //    await DisplayAlert("Warning", "Please enter Notes", "Cancel");
            //}
            //if (string.IsNullOrEmpty(endDate))
            //{
            //    await DisplayAlert("Warning", "Please enter Notes", "Cancel");
            //}

            //MedicineModel medicineModel = new MedicineModel();
            Preferences.Set("MedName", MedName);
            Preferences.Set("startDate", startDate);
            Preferences.Set("endDate", endDate);
            //medicineModel.med_StartDate = ;
            //medicineModel.med_EndDate = ;
            //medicineModel.med_Strength = Preferences.Get("strength", "");
            //medicineModel.med_Unit = Preferences.Get("priority", "");
            //medicineModel.email = Preferences.Get("userEmail", "default");

            if (file != null)
            {
                string image = await medicineRepository.upload(file.GetStream(), Path.GetFileName(file.Path));
                //medicineModel.Image = image;
                Preferences.Set("Image", image);
            }

            //var isSaved = await medicineRepository.Save(medicineModel);
            //if (isSaved)
            //{
            //await DisplayAlert("Information", "Medicine succussful save", "Ok");
            await Navigation.PushAsync(new DoseTimePage());
            //Clear();
            //}
            //else
            //{
            //    await DisplayAlert("Warning", "Medicine saved fail", "Ok");
            //}
        }

        private async void cameraIcon_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
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
                //var stream = await result.OpenReadAsync();
                //if (file == null) return;
                //medicineImage.Source = ImageSource.FromStream(() => stream);
            }
            catch (Exception ex)
            {

            }
        }
    }
}