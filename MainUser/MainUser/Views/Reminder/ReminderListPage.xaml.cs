﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views.Reminder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReminderListPage : ContentPage
    {
        ReminderRepository reminderRepository = new ReminderRepository();
        public ReminderListPage()
        {
            bool haskey = Preferences.ContainsKey("token");
            if (haskey)
            {
                InitializeComponent();
                ReminderListView.RefreshCommand = new Command(() =>
                {
                    OnAppearing();
                });
            }
        }

        protected override async void OnAppearing()
        {
            var email = Preferences.Get("PatientEmail", null);
            if (!string.IsNullOrEmpty(email))
            {
                email = Preferences.Get("PatientEmail", "");
            }
            else
            {
                email = Preferences.Get("userEmail", "");
            }
            var reminder = await reminderRepository.GetAllUncompleted(email);
            ReminderListView.ItemsSource = null;
            lblNoRecord.IsVisible = false;
            if (reminder.Count != 0)
            {
                ReminderListView.ItemsSource = reminder;
            }
            else
            {
                lblNoRecord.IsVisible = true;
            }
            ReminderListView.IsRefreshing = false;
        }

        private async void AddReminderButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ReminderEntry());
        }

        //private void ReminderListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //    {
        //        return;
        //    }

        //    var reminder = e.Item as ReminderModel;
        //    Navigation.PushAsync(new ReminderDetail(reminder));
        //    ((ListView)sender).SelectedItem = null;

        //}

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var reminder = await reminderRepository.GetById(id);

            if (reminder == null)
            {
                await DisplayAlert("Warning", "Data not found", "Ok");
                return;
            }
            reminder.ID = id;
            await Navigation.PushModalAsync(new ReminderEdit(reminder));
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
                    return;
                }
            }
        }

        private async void completedTap_Tapped(object sender, EventArgs e)
        {
            //UncheckedComplete.Source = ImageSource.FromFile("fileName");
            var response = await DisplayAlert("Delete", "Do you want to complete task ? ", "Yes", "No");

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
                reminders.status = "Completed";
                reminders.email = reminder.email;
                reminders.setDate = reminder.setDate;
                reminders.setTime = reminder.setTime;
                reminders.completeDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

                bool isUpdated = await reminderRepository.Update(reminders);

                if (isUpdated)
                {
                    await DisplayAlert("Information", "Reminder has been complete", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Warning", "Reminder Deleted Failed", "Ok");
                    return;
                }
            }
        }

        private void btmHistory_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CompleteHistoryList());
        }
    }
}
