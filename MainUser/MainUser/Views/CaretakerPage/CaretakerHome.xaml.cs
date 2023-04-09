using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            PatientListView.ItemsSource = userTypeList;
            PatientListView.IsRefreshing = false;
        }

        private void userTypeList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var userID = e.Item as UserTypeModel;
            Navigation.PushAsync(new MenuPatient(userID));
            ((ListView)sender).SelectedItem = null;

        }
    }
}