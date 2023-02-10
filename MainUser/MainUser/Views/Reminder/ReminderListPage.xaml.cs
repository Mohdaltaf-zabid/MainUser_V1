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
    public partial class ReminderListPage : ContentPage
    {
        ReminderRepository reminderRepository = new ReminderRepository();
        public ReminderListPage()
        {
            InitializeComponent();
            ReminderListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var reminder = await reminderRepository.GetAll();
            ReminderListView.ItemsSource = null;
            ReminderListView.ItemsSource = reminder;
            ReminderListView.IsRefreshing = false;
        }

        private void AddReminderButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ReminderEntry());
        }

        private void ReminderListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var reminder = e.Item as ReminderModel;
            Navigation.PushModalAsync(new ReminderDetail(reminder));
            ((ListView)sender).SelectedItem = null;

        }

        private async void DeleteTap_Tapped(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete", "Do you want to delete ? ", "Yes", "No");

            if (response)
            {
                string ID = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await reminderRepository.Delete(ID);
                if (isDelete)
                {
                    await DisplayAlert("Information", "Student has been Deleted", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Warning", "Student Deleted Failed", "Ok");
                }
            }
        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Edit", "Do you want to edit", "Ok");

            string id = ((TappedEventArgs)e).Parameter.ToString();
            var reminder = await reminderRepository.GetById(id);

            if (reminder == null)
            {
                await DisplayAlert("Warning", "Data not found", "Ok");
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
                    await DisplayAlert("Information", "Student has been Deleted", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Warning", "Student Deleted Failed", "Ok");
                }
            }
        }
    }
}
