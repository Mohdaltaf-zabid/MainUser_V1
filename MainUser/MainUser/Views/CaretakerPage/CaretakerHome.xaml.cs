using MainUser.Views.Reminder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views.CaretakerPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaretakerHome : ContentPage
    {
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public CaretakerHome()
        {
            InitializeComponent();
            PatientListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var userTypeList = await userTypeRepository.GetPatientApproveList();
            PatientListView.ItemsSource = null;
            if (userTypeList.Count != 0)
            {
                PatientListView.ItemsSource = userTypeList;

            }
            else
            {
                lblNoRecord.IsVisible = true;
            }
            PatientListView.IsRefreshing = false;
        }

        private void userTypeList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var userID = e.Item as UserTypeModel;
            Navigation.PushModalAsync(new MenuPatient(userID));
            ((ListView)sender).SelectedItem = null;

        }

        private void btmAddPatient_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddPatient());
        }
    }
}