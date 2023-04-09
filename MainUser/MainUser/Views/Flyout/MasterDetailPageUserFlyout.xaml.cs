using MainUser.Views.Medicine;
using MainUser.Views.Reminder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MainUser.Views.Flyout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageUserFlyout : ContentPage
    {
        public ListView ListView;

        public MasterDetailPageUserFlyout()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPageUserFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailPageUserFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPageUserFlyoutMenuItem> MenuItems { get; set; }

            public MasterDetailPageUserFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPageUserFlyoutMenuItem>(new[]
                {
                    new MasterDetailPageUserFlyoutMenuItem { Id = 0, Title = "Medicine",TargetType=typeof(MedicineListPage)  },
                    new MasterDetailPageUserFlyoutMenuItem { Id = 1, Title = "Reminder" , TargetType = typeof(ReminderListPage) },
                    new MasterDetailPageUserFlyoutMenuItem { Id = 2, Title = "Request caretaker",TargetType=typeof(RequestCaretakerPage)  },
                    //new MasterDetailPageUserFlyoutMenuItem { Id = 3, Title = "Logout", TargetType=typeof(LoginPage)},
                    //new MasterDetailPageUserFlyoutMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}