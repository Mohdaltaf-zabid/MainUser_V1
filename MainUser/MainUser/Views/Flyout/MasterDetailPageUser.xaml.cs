using MainUser.Views.FlyoutCaretaker;
using MainUser.Views.Medicine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Flyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageUser : FlyoutPage
    {
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public MasterDetailPageUser()
        {
            InitializeComponent();
            //bool haskey = Preferences.ContainsKey("token");
            //if (haskey)
            //{
            //    string token = Preferences.Get("token", "");
            //    if (!string.IsNullOrEmpty(token))
            //    {
            //
            //        ();
            //    }
            //}
            FlyoutPage.ListView.ItemSelected += ListView_ItemSelected;
            Detail = new NavigationPage(new MedicineListPage());
        }

        //private async void startup()
        //{
        //    var usertype = await userTypeRepository.GetByEmail();
        //    string userType = usertype.userType;
        //    string userfullName = usertype.fullName;
        //    if (!string.IsNullOrEmpty(userType))
        //    {
        //        Preferences.Set("fullName", userfullName);

        //        if (userType == "Family member/caretaker")
        //        {
        //            await Navigation.PushModalAsync(new MasterDetailPageCaretaker());
        //        }
        //    }
        //}

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterDetailPageUserFlyoutMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            FlyoutPage.ListView.SelectedItem = null;
        }

        private async void LogoutToolbarItem_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            await Navigation.PopModalAsync();
        }
    }
}