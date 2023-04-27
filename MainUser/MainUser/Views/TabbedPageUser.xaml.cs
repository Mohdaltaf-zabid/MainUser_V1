using MainUser.Views.FlyoutCaretaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageUser : TabbedPage
    {
        UserTypeRepository userTypeRepository = new UserTypeRepository();
        public TabbedPageUser()
        {
            InitializeComponent();
            bool haskey = Preferences.ContainsKey("token");
            string loop = Preferences.Get("loop","false");

            if (haskey)
            {
                if (loop =="false")
                {
                    string token = Preferences.Get("token", "");
                    if (!string.IsNullOrEmpty(token))
                    {
                        startup();
                        Preferences.Set("loop", "true");
                    }
                }
            }
            else
            {
                Navigation.PushModalAsync(new LoginPage());
            }
        }
        private async void startup()
        {
            var usertype = await userTypeRepository.GetByEmail();
            string userType = usertype.userType;
            string userfullName = usertype.fullName;
            if (!string.IsNullOrEmpty(userType))
            {
                Preferences.Set("fullName", userfullName);

                if (userType == "Normal User/patient")
                {
                    //await Navigation.PushModalAsync(new TabbedPageUser());
                }
                else if (userType == "Family member/caretaker")
                {
                    await Navigation.PushModalAsync(new TabbedPageCaretaker());
                }
            }
        }
    }
}