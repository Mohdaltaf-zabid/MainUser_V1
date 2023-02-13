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
            //TxtSetdate.Text = reminder.setDate;
            //TxtSetTime.Text = reminder.setTime;
            TxtID.Text = reminder.ID;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string title = TxtTitle.Text;
            string notes = TxtNotes.Text;
            string priority = pickerCountry.SelectedItem as String;
            //string repeat = TxtRepeat.Text;
            //string setDate = TxtSetdate.Text;
            //string setTime = TxtSetTime.Text;

            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Warning", "Please enter Title", "Cancel");
            }
            if (string.IsNullOrEmpty(notes))
            {
                await DisplayAlert("Warning", "Please enter Notes", "Cancel");
            }
            if (pickerCountry.SelectedIndex == -1)
            {
                await DisplayAlert("Warning", "Please enter Priority", "Cancel");
            }
            //if (string.IsNullOrEmpty(repeat))
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
            //}

            ReminderModel reminder = new ReminderModel();
            reminder.ID = TxtID.Text;
            reminder.title = title;
            reminder.notes = notes;
            reminder.priority = priority;
            //reminder.repeat = repeat;
            //reminder.setDate = setDate;
            //reminder.setTime = setTime;
            reminder.email = Preferences.Get("userEmail", "default");
            bool isUpdated = await reminderRepository.Update(reminder);

            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Update failed", "Cancel");
            }
        }
    }
}