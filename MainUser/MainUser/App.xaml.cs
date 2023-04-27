using MainUser.Views;
using MainUser.Views.Reminder;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Preferences.Remove("loop");
            MainPage = new TabbedPageUser();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
