using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Reminder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderEdit : ContentPage
    {
        ReminderRepository reminderRepository = new ReminderRepository();
        public ReminderEdit(ReminderModel reminder)
        {
            InitializeComponent();
            TxtTitle.Text = reminder.title;
            TxtNotes.Text = reminder.notes;
            pickerCountry.SelectedItem = reminder.priority;
            //TxtRepeat.Text = reminder.repeat;
            TxtSetdate.Date = reminder.setDate;
            TxtSetTime.Time = reminder.setTime;
            TxtID.Text = reminder.ID;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string title = TxtTitle.Text;
            string notes = TxtNotes.Text;
            string priority = pickerCountry.SelectedItem as String;
            //string repeat = TxtRepeat.Text;
            DateTime setDate = TxtSetdate.Date;
            TimeSpan setTime = TxtSetTime.Time;

            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Warning", "Please enter Title", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(notes))
            {
                await DisplayAlert("Warning", "Please enter Notes", "Ok");
                return;
            }
            if (pickerCountry.SelectedIndex == -1)
            {
                await DisplayAlert("Warning", "Please enter Priority", "Ok");
                return;
            }
            /*if (string.IsNullOrEmpty(repeat))
            //{
            //    await DisplayAlert("Warning", "Please enter repeat", "Cancel");
            //}
            //if (string.IsNullOrEmpty(setDate))
            //{
            //    await DisplayAlert("Warning", "Please enter set Date", "Cancel");
            //}
            //if (string.IsNullOrEmpty(setTime))
            //{
            //    await DisplayAlert("Warning", "Please enter set Time", "Cancel");
            }*/

            ReminderModel reminder = new ReminderModel();
            reminder.ID = TxtID.Text;
            reminder.title = title;
            reminder.notes = notes;
            reminder.priority = priority;
            reminder.status = "Uncompleted";
            //reminder.repeat = repeat;
            reminder.setDate = setDate;
            reminder.setTime = setTime;
            reminder.email = Preferences.Get("userEmail", "default");
            bool isUpdated = await reminderRepository.Update(reminder);

            if (isUpdated)
            {
                await DisplayAlert("Information", "Reminder succussful edit", "Ok");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Update failed", "Ok");
                return;
            }
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}