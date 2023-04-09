using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Reminder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompleteHistoryList : ContentPage
    {
        ReminderRepository reminderRepository = new ReminderRepository();
        public CompleteHistoryList()
        {
            InitializeComponent();
            CompleteReminderListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var reminder = await reminderRepository.Getcompleted();
            CompleteReminderListView.ItemsSource = null;
            CompleteReminderListView.ItemsSource = reminder;
            CompleteReminderListView.IsRefreshing = false;
        }

        private async void DeleteSwipeItem_Invoked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete", "Do you want to delete ? ", "Yes", "No");

            if (response)
            {
                string ID = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await reminderRepository.Delete(ID);
                if (isDelete)
                {
                    await DisplayAlert("Information", "Reminder has been Deleted", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Warning", "Reminder Deleted Failed", "Ok");
                }
            }
        }

        private async void UncompletedTap_Tapped(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete", "Do you want to Uncomplete task ? ", "Yes", "No");

            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                var reminder = await reminderRepository.GetById(id);

                if (reminder == null)
                {
                    await DisplayAlert("Warning", "Data not found", "Ok");
                    return;
                }

                ReminderModel reminders = new ReminderModel();
                reminders.ID = id;
                reminders.title = reminder.title;
                reminders.notes = reminder.notes;
                reminders.priority = reminder.priority;
                reminders.status = "Uncompleted";
                reminders.setDate = reminder.setDate;
                reminders.setTime = reminder.setTime;
                reminders.email = reminder.email;

                bool isUpdated = await reminderRepository.Update(reminders);

                if (isUpdated)
                {
                    await DisplayAlert("Information", "Reminder has been Uncomplete", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Warning", "Reminder Deleted Failed", "Ok");
                }
            }
        }
    }
}