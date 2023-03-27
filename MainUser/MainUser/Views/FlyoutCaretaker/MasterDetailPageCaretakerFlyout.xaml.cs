using MainUser.Views.CaretakerPage;
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

namespace MainUser.Views.FlyoutCaretaker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPageCaretakerFlyout : ContentPage
    {
        public ListView ListView;

        public MasterDetailPageCaretakerFlyout()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPageCaretakerFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailPageCaretakerFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPageCaretakerFlyoutMenuItem> MenuItems { get; set; }
            
            public MasterDetailPageCaretakerFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPageCaretakerFlyoutMenuItem>(new[]
                {
                    new MasterDetailPageCaretakerFlyoutMenuItem { Id = 0, Title = "List of patient", TargetType=typeof(CaretakerHome) },
                    new MasterDetailPageCaretakerFlyoutMenuItem { Id = 1, Title = "Add patient" , TargetType=typeof(AddPatient) },
                    //new MasterDetailPageCaretakerFlyoutMenuItem { Id = 2, Title = "Page 3" },
                    //new MasterDetailPageCaretakerFlyoutMenuItem { Id = 3, Title = "Page 4" },
                    //new MasterDetailPageCaretakerFlyoutMenuItem { Id = 4, Title = "Page 5" },
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