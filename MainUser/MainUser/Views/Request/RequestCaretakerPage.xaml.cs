﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RequestCaretakerPage : ContentPage
    {
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public ICommand ButtonCommand => new Command<string>(ButtonApprovePatient);
        public RequestCaretakerPage()
        {
            InitializeComponent();
            BindingContext = this;
            AddCaretakerListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var CaretakerAddList = await userTypeRepository.GetPatientPendingList();
            AddCaretakerListView.ItemsSource = null;
            AddCaretakerListView.ItemsSource = CaretakerAddList;
            AddCaretakerListView.IsRefreshing = false;

        }
        public async void ButtonApprovePatient(string str)
        {
            var response = await DisplayAlert("Approved", "Do you want to approve caretaker ? ", "Yes", "No");

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
                userTypeModel.status = "Approved";
                userTypeModel.caretakerEmail = user.caretakerEmail;

                bool isUpdated = await userTypeRepository.UpdatePatient(userTypeModel);

                if (isUpdated)
                {
                    await DisplayAlert("Update", "Request has confirmed", "Ok");
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