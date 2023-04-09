using MainUser.Views.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views.Medicine
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MedUpdateStatus : ContentPage
    {
        string medID;
        MedicineRepository medicineRepository = new MedicineRepository();

        public MedUpdateStatus(MedicineModel medicineModel)
        {
            InitializeComponent();
            string statusTime = medicineModel.med_statusTime.ToString();
            if (statusTime == "1/1/0001 12:00:00 AM")
            {
                statusTime = null;
            }
            lblNameMed.Text = "Name: " + medicineModel.med_Name;
            lblStrengthMed.Text = "Strength: " + medicineModel.med_Strength + " " + medicineModel.med_Unit;
            lblStatusMed.Text = "Status: " + medicineModel.med_status + " " + statusTime;
            medID = medicineModel.ID;
            TxtRemark.Text = medicineModel.med_remark;
        }

        private async void btnSkip_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Update", "Do you want skip ? ", "Yes", "No");

            if (response)
            {
                process("Skip");
            }
        }

        private async void btnDone_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Update", "Do you want done ? ", "Yes", "No");

            if (response)
            {
                process("Done");
            }
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete", "Do you want to delete ? ", "Yes", "No");

            if (response)
            {
                string ID = medID;
                bool isDelete = await medicineRepository.Delete(ID);
                if (isDelete)
                {
                    await DisplayAlert("Information", "Medicine has been Deleted", "Ok");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Warning", "Reminder Deleted Failed", "Ok");
                }
            }
        }


        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string id = medID;
            string remark = TxtRemark.Text;
            var medicine = await medicineRepository.GetById(id);

            if (string.IsNullOrEmpty(remark))
            {
                await DisplayAlert("Warning", "Please enter Title", "Ok");
                return;
            }
            if (medicine == null)
            {
                await DisplayAlert("Warning", "Data not found", "Ok");
                return;
            }

            MedicineModel medicineModel = new MedicineModel();
            medicineModel.ID = id;
            medicineModel.med_Name = medicine.med_Name;
            medicineModel.med_StartDate = medicine.med_StartDate;
            medicineModel.med_EndDate = medicine.med_EndDate;
            medicineModel.med_Day = medicine.med_Day;
            medicineModel.med_Strength = medicine.med_Strength;
            medicineModel.med_Unit = medicine.med_Unit;
            medicineModel.email = medicine.email;
            medicineModel.med_remark = remark;
            medicineModel.med_status = medicine.med_status;
            medicineModel.med_statusTime = medicine.med_statusTime;

            bool isUpdated = await medicineRepository.Update(medicineModel);

            if (isUpdated)
            {
                await DisplayAlert("Information", "Medicine has been Updated", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Warning", "Medicine Updated Failed", "Ok");
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void process(string status)
        {
            string id = medID;
            var medicine = await medicineRepository.GetById(id);

            if (medicine == null)
            {
                await DisplayAlert("Warning", "Data not found", "Ok");
                return;
            }

            MedicineModel medicineModel = new MedicineModel();
            medicineModel.ID = id;
            medicineModel.med_Name = medicine.med_Name;
            medicineModel.med_StartDate = medicine.med_StartDate;
            medicineModel.med_EndDate = medicine.med_EndDate;
            medicineModel.med_Day = medicine.med_Day;
            medicineModel.med_Strength = medicine.med_Strength;
            medicineModel.med_Unit = medicine.med_Unit;
            medicineModel.email = medicine.email;
            medicineModel.med_remark = medicine.med_remark;
            medicineModel.med_status = status;
            medicineModel.med_statusTime = DateTime.Now;

            bool isUpdated = await medicineRepository.Update(medicineModel);

            if (isUpdated)
            {
                await DisplayAlert("Information", "Medicine has been Updated", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Warning", "Medicine Updated Failed", "Ok");
            }
        }
    }
}