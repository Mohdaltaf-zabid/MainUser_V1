using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Reminder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderEntry : ContentPage
    {
        ReminderRepository repository = new ReminderRepository();
        public ReminderEntry()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string title = TxtTitle.Text;
            string notes = TxtNotes.Text;
            string priority = TxtPriority.Text;
            string repeat = TxtRepeat.Text;
            string setDate = TxtSetdate.Text;
            string setTime = TxtSetTime.Text;

            if (string.IsNullOrEmpty(title))
            {
                await DisplayAlert("Warning", "Please enter Title", "Cancel");
            }
            if (string.IsNullOrEmpty(notes))
            {
                await DisplayAlert("Warning", "Please enter Notes", "Cancel");
            }
            if (string.IsNullOrEmpty(priority))
            {
                await DisplayAlert("Warning", "Please enter Priority", "Cancel");
            }
            if (string.IsNullOrEmpty(repeat))
            {
                await DisplayAlert("Warning", "Please enter repeat", "Cancel");
            }
            if (string.IsNullOrEmpty(setDate))
            {
                await DisplayAlert("Warning", "Please enter set Date", "Cancel");
            }
            if (string.IsNullOrEmpty(setTime))
            {
                await DisplayAlert("Warning", "Please enter set Time", "Cancel");
            }

            ReminderModel reminder = new ReminderModel();
            reminder.title = title;
            reminder.notes = notes;
            reminder.priority = priority;
            reminder.repeat = repeat;
            reminder.setDate = setDate;
            reminder.setTime = setTime;

            var isSaved = await repository.Save(reminder);
            if (isSaved)
            {
                await DisplayAlert("Information", "Reminder succussful save", "Ok");
                Clear();
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
            TxtPriority.Text = string.Empty;
            TxtRepeat.Text = string.Empty;
            TxtSetdate.Text = string.Empty;
            TxtSetTime.Text = string.Empty;
        }
    }
}