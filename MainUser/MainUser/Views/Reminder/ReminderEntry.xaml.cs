using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views.Reminder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderEntry : ContentPage
    {
        ReminderRepository repository = new ReminderRepository();

        public ReminderEntry()
        {
            InitializeComponent();
            pickerCountry.SelectedIndex = 0;
            TxtSetTime.Time = DateTime.Now.TimeOfDay;
            Preferences.Get("date", "");
            Preferences.Get("time", "");
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string title = TxtTitle.Text;
            string notes = TxtNotes.Text;
            string priority = pickerCountry.SelectedItem as String;
            //string repeat = TxtRepeat.Text;
            TimeSpan setTime = TxtSetTime.Time;
            DateTime setDate = TxtSetdate.Date;

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
            //if (DateTime.(setDate))
            //{
            //    await DisplayAlert("Warning", "Please enter set Date", "Cancel");
            //}
            //if (string.IsNullOrEmpty(setTime))
            //{
            //    await DisplayAlert("Warning", "Please enter set Time", "Cancel");
            //}

            ReminderModel reminder = new ReminderModel();
            reminder.title = title;
            reminder.notes = notes;
            reminder.priority = priority;
            reminder.status = "Uncompleted";

            string date = Preferences.Get("date", null);
            string time = Preferences.Get("time", null);
            if (!string.IsNullOrEmpty(date))
            {
                reminder.setDate = setDate;
            }

            if (!string.IsNullOrEmpty(time))
            {
                reminder.setTime = setTime;
            }
            reminder.email = Preferences.Get("userEmail", "default");

            var isSaved = await repository.Save(reminder);
            if (isSaved)
            {
                await DisplayAlert("Information", "Reminder succussful save", "Ok");
                await Navigation.PopModalAsync();
                //Clear();
            }
            else
            {
                await DisplayAlert("Warning", "Reminder saved fail", "Ok");
            }
        }

        public void Clear()
        {
            TxtTitle.Text = string.Empty;
            TxtNotes.Text = string.Empty;
            pickerCountry.SelectedIndex = 0;
            //TxtRepeat.Text = string.Empty;
            TxtSetdate.Date = DateTime.Now;
            TxtSetTime.Time = DateTime.Now.TimeOfDay;
        }

        private void TxtSetdate_Focused(object sender, FocusEventArgs e)
        {

            Preferences.Set("date", "1");
        }

        private void TxtSetTime_Focused(object sender, FocusEventArgs e)
        {
            Preferences.Set("time", "1");
        }

        private async void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}