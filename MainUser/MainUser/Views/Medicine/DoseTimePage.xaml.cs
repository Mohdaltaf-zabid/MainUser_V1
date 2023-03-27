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

namespace MainUser.Views.Medicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DoseTimePage : ContentPage
    {
        DateTime startDate, endDate;
        MedicineRepository medicineRepository = new MedicineRepository();
        public DoseTimePage()
        {
            InitializeComponent();
        }

        private async void ButtonSaveDoseTime_Clicked(object sender, EventArgs e)
        {
            MedicineModel medicineModel = new MedicineModel();
            medicineModel.med_Name = Preferences.Get("MedName", "");
            medicineModel.med_StartDate = Preferences.Get("startDate", startDate);
            medicineModel.med_EndDate = Preferences.Get("endDate", endDate);
            medicineModel.med_Strength = Preferences.Get("strength", "");
            medicineModel.med_Unit = Preferences.Get("priority", "");
            medicineModel.email = Preferences.Get("userEmail", "default");

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

            var isSaved = await medicineRepository.Save(medicineModel);
            if (isSaved)
            {
                await DisplayAlert("Information", "Medicine succussful save", "Ok");
            }
            else
            {
                await DisplayAlert("Warning", "Medicine saved fail", "Ok");
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
                    visibleTimeADayTrue(false);
                    gridTimeDose.IsVisible = false;
                    lblTimeAndDose.IsVisible = false;
                    pickerTimeADay.SelectedIndex = -1;

                }
                else
                {
                    visibleTimeADayTrue(true);
                }
            }
        }

        private void pickerTimeADay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerTimeADay.SelectedIndex != -1)
            {
                gridTimeDose.IsVisible = true;
                lblTimeAndDose.IsVisible = true;
                if (pickerTimeADay.SelectedIndex == 0)
                {
                    visibleFirstDose(true);
                    visibleSecondDose(false);
                    visibleThirdDose(false);
                    visibleForthDose(false);
                    visibleFifthDose(false);
                    visibleSixthDose(false);
                }
                else if (pickerTimeADay.SelectedIndex == 1)
                {
                    visibleFirstDose(true);
                    visibleSecondDose(true);
                    visibleThirdDose(false);
                    visibleForthDose(false);
                    visibleFifthDose(false);
                    visibleSixthDose(false);
                }
                else if (pickerTimeADay.SelectedIndex == 2)
                {
                    visibleFirstDose(true);
                    visibleSecondDose(true);
                    visibleThirdDose(true);
                    visibleForthDose(false);
                    visibleFifthDose(false);
                    visibleSixthDose(false);
                }
                else if (pickerTimeADay.SelectedIndex == 3)
                {
                    visibleFirstDose(true);
                    visibleSecondDose(true);
                    visibleThirdDose(true);
                    visibleForthDose(true);
                    visibleFifthDose(false);
                    visibleSixthDose(false);
                }
                else if (pickerTimeADay.SelectedIndex == 4)
                {
                    visibleFirstDose(true);
                    visibleSecondDose(true);
                    visibleThirdDose(true);
                    visibleForthDose(true);
                    visibleFifthDose(true);
                    visibleSixthDose(false);
                }
                else if (pickerTimeADay.SelectedIndex == 5)
                {
                    visibleFirstDose(true);
                    visibleSecondDose(true);
                    visibleThirdDose(true);
                    visibleForthDose(true);
                    visibleFifthDose(true);
                    visibleSixthDose(true);
                }
            }
        }

        private void visibleTimeADayTrue(bool visble)
        {
            lblTimeADay.IsVisible = visble;
            pickerTimeADay.IsVisible = visble;
        }
        private void visibleFirstDose(bool visble)
        {
            tmrTimeFirst.IsVisible = visble;
            lblFirstStepper.IsVisible = visble;
            SFirst.IsVisible = visble;
        }
        private void visibleSecondDose(bool visble)
        {
            tmrTimeSecond.IsVisible = visble;
            lblSecondStepper.IsVisible = visble;
            SSecond.IsVisible = visble;
        }
        private void visibleThirdDose(bool visble)
        {
            tmrTimeThird.IsVisible = visble;
            lblThirdStepper.IsVisible = visble;
            SThird.IsVisible = visble;
        }
        private void visibleForthDose(bool visble)
        {
            tmrTimeFourth.IsVisible = visble;
            lblForthStepper.IsVisible = visble;
            SForth.IsVisible = visble;
        }
        private void visibleFifthDose(bool visble)
        {
            tmrTimeFifth.IsVisible = visble;
            lblFifthStepper.IsVisible = visble;
            SFifth.IsVisible = visble;
        }
        private void visibleSixthDose(bool visble)
        {
            tmrTimesixth.IsVisible = visble;
            lblSixthStepper.IsVisible = visble;
            SSixth.IsVisible = visble;
        }
    }
}