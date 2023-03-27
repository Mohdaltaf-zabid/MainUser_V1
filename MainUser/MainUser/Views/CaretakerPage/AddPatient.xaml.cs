using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MainUser.Views.CaretakerPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPatient : ContentPage
    {
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public ICommand ButtonCommand => new Command<string>(ButtonAddPatient);
        public AddPatient()
        {
            InitializeComponent();
            BindingContext = this;
            AddPatientListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var userAddList = await userTypeRepository.GetPatientList();
            AddPatientListView.ItemsSource = null;
            AddPatientListView.ItemsSource = userAddList;
            AddPatientListView.IsRefreshing = false;

        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string userAddList = TxtSearch.Text;
            if (!string.IsNullOrEmpty(userAddList))
            {
                var userlist = await userTypeRepository.GetUserList(userAddList);
                AddPatientListView.ItemsSource = null;
                AddPatientListView.ItemsSource = userlist;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string userAddList = TxtSearch.Text;
            if (!string.IsNullOrEmpty(userAddList))
            {
                var userlist = await userTypeRepository.GetUserList(userAddList);
                AddPatientListView.ItemsSource = null;
                AddPatientListView.ItemsSource = userlist;
            }
            else
            {
                OnAppearing();
            }
        }

        public async void ButtonAddPatient(string str)
        {
            var response = await DisplayAlert("Add user", "Do you want to add user ? ", "Yes", "No");

            if (response)
            {
                string id = str;

                var user = await userTypeRepository.GetPatientyId(id);

                if (user == null)
                {
                    await DisplayAlert("Warning", "Data not found", "Ok");
                }

                UserTypeModel userTypeModel = new UserTypeModel();
                userTypeModel.ID = str;
                userTypeModel.fullName = user.fullName;
                userTypeModel.userType = user.userType;
                userTypeModel.email = user.email;
                userTypeModel.status = "Request";
                userTypeModel.caretakerEmail = Preferences.Get("userEmail", "default");

                bool isUpdated = await userTypeRepository.UpdatePatient(userTypeModel);

                if (isUpdated)
                {
                    await DisplayAlert("Update", "Request has sent", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Warning", "Request Failed", "Ok");
                }
            }
        }
    }
}